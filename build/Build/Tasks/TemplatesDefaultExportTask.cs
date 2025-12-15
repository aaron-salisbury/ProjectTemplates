using Build.DTOs;
using Build.Tasks.Standard;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static Build.BuildContext;

namespace Build.Tasks;

[TaskName("Export Default Project Templates")]
[IsDependentOn(typeof(CompileProjectsTask))]
[TaskDescription("Attempts to mirror Visual Studio's 'Export Template Wizard' in generating a project template from every project in the source directory, besides the VS extension project.")]
public sealed class TemplatesDefaultExportTask : FrostingTask<BuildContext>
{
    public override bool ShouldRun(BuildContext context)
    {
        return context.Config == BuildConfigurations.Release;
    }

    public override void Run(BuildContext context)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        context.Log.Information($"Exporting default project templates...");

        foreach (TemplateProject templateProject in context.TemplateProjects)
        {
            string templateStagingDir = Path.Combine(templateProject.OutputDirectoryPathAbsolute, templateProject.Name);

            if (ExportDefaultTemplate(templateProject, templateStagingDir, context))
            {
                CapitalizeFirstLevelFolders(templateStagingDir);
                CompressDirectory(templateStagingDir);
            }
            else
            {
                context.Log.Error($"[FAIL] Failed to export default template for project: {templateProject.CsprojFilePathAbsolute}");
            }
        }

        stopwatch.Stop();
        double completionTime = Math.Round(stopwatch.Elapsed.TotalSeconds, 1);
        context.Log.Information($"Export of default project templates complete ({completionTime}s)");
    }

    private static bool ExportDefaultTemplate(TemplateProject templateProject, string templateStagingDir, BuildContext context)
    {
        try
        {
            // Clean up any previous staging
            if (Directory.Exists(templateStagingDir))
            {
                Directory.Delete(templateStagingDir, recursive: true);
            }

            // Copy all files except bin/obj/.vs/.git
            CopyProjectFiles(templateProject.DirectoryPathAbsolute, templateStagingDir);

            // Apply parameters.
            ApplySafeProjectNameToFiles(templateProject.CsprojFilePathAbsolute, templateStagingDir, templateProject.IsApplication);
            ReplaceProjectGuidWithTemplateParameter(Path.Combine(templateStagingDir, Path.GetFileName(templateProject.CsprojFilePathAbsolute)), templateProject.IsSdkStyleProject);

            // Create the .vstemplate file.
            string vsTemplateXML = CreateVSTemplateContents(templateProject.CsprojFilePathAbsolute, templateStagingDir, templateProject.IsSdkStyleProject, context);
            string vstemplatePath = Path.Combine(templateStagingDir, "MyTemplate.vstemplate");
            File.WriteAllText(vstemplatePath, vsTemplateXML);

            // Copy default template icon to staging directory.
            string contentDir = Path.Combine(context.AbsolutePathToRepo, "content");
            string iconSourcePath = Path.Combine(contentDir, "__TemplateIcon.ico");
            string iconDestinationPath = Path.Combine(templateStagingDir, "__TemplateIcon.ico");
            if (File.Exists(iconSourcePath))
            {
                File.Copy(iconSourcePath, iconDestinationPath, overwrite: true);
            }
            else
            {
                context.Log.Error($"Default template icon file not found at {iconSourcePath}");
            }

            return true;
        }
        catch (Exception ex)
        {
            context.Log.Error($"[FAIL] Exception while staging template for {templateProject.CsprojFilePathAbsolute}: {ex.Message}");
            return false;
        }
    }

    private static void CopyProjectFiles(string projectDirectory, string stagingDirectory)
    {
        // Copy all files except bin/obj/.vs/.git
        foreach (string file in Directory.EnumerateFiles(projectDirectory, "*", SearchOption.AllDirectories))
        {
            string relativePath = Path.GetRelativePath(projectDirectory, file);
            if (relativePath.StartsWith("bin") || relativePath.StartsWith("obj") ||
                relativePath.StartsWith(".vs") || relativePath.StartsWith(".git"))
            {
                continue;
            }

            string destinationFile = Path.Combine(stagingDirectory, relativePath);
            Directory.CreateDirectory(Path.GetDirectoryName(destinationFile)!);
            File.Copy(file, destinationFile, overwrite: true);
        }
    }

    private static void ApplySafeProjectNameToFiles(string csprojPath, string stagingDirectory, bool isProjectAnApplication)
    {
        string fullProjectName = Path.GetFileNameWithoutExtension(csprojPath);

        string replacementName = PARAM_SAFE_PROJECT_NAME_PARENT;
        if (isProjectAnApplication && !fullProjectName.Contains('.'))
        {
            // The ProjectTemplates solution projects are designed to be three-tier. The business and data tiers are named accordingly, 
            // but for simplicity & clarity the presentation tier (apps w/ platform-UI specific frameworks) are all named after the platform they target (e.g., WinXPApp).
            // However, when users of the extension export templates, all tiers being suffixed is the expectation.
            replacementName += ".Presentation";
        }

        // We want something like MyCoolApp.Presentation.Web to become $ext_safeprojectname$.Presentation.Web
        int firstDot = fullProjectName.IndexOf('.');
        string projectNamePrefix = firstDot > 0 ? fullProjectName[..firstDot] : fullProjectName;

        // Regex: Match the prefix only if followed by a non-word char, dot, semicolon, or end of string.
        string pattern = $@"\b{Regex.Escape(projectNamePrefix)}(?=[\s\.;:,<>\[\]\(\)""'/\\]|$)";

        foreach (string file in Directory.EnumerateFiles(stagingDirectory, "*", SearchOption.AllDirectories))
        {
            try
            {
                string content = File.ReadAllText(file);
                // Only do replacement if the file appears to be text.
                if (!content.Contains('\0')) // Crude check for binary.
                {
                    string replaced = Regex.Replace(content, pattern, replacementName, RegexOptions.IgnoreCase);
                    if (!ReferenceEquals(content, replaced))
                    {
                        File.WriteAllText(file, replaced);
                    }
                }
            }
            catch
            {
                // Ignore files that can't be read as text.
                continue;
            }
        }
    }

    private static string CreateVSTemplateContents(string csprojPath, string stagingDirectory, bool isSdkStyle, BuildContext context)
    {
        string projectName = Path.GetFileNameWithoutExtension(csprojPath);
        string csprojFileName = Path.GetFileName(csprojPath);
        IEnumerable<(string id, string version)> nugetPackages = ReadProjectNuGetPackages(csprojPath, isSdkStyle, context);

        // Build the folder/file structure recursively
        DTOs.Project BuildProject(string dir)
        {
            DTOs.Project project = new()
            {
                TargetFileName = csprojFileName,
                File = csprojFileName,
                ReplaceParameters = true,
                Items = []
            };

            foreach (string folder in Directory.GetDirectories(dir))
            {
                project.Items.Add(BuildFolder(folder));
            }
            foreach (string file in Directory.GetFiles(dir))
            {
                string fileName = Path.GetFileName(file);
                if (fileName.Equals(csprojFileName, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                project.Items.Add(new ProjectItem
                {
                    ReplaceParameters = true,
                    TargetFileName = fileName,
                    Value = fileName
                });
            }
            return project;
        }

        DTOs.Folder BuildFolder(string dir)
        {
            string folderName = Path.GetFileName(dir);
            DTOs.Folder folder = new()
            {
                Name = folderName,
                TargetFolderName = folderName,
                Items = []
            };

            foreach (string subfolder in Directory.GetDirectories(dir))
            {
                folder.Items.Add(BuildFolder(subfolder));
            }
            foreach (string file in Directory.GetFiles(dir))
            {
                string fileName = Path.GetFileName(file);
                if (fileName.Equals(csprojFileName, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                folder.Items.Add(new DTOs.ProjectItem
                {
                    ReplaceParameters = true,
                    TargetFileName = fileName,
                    Value = fileName
                });
            }
            return folder;
        }

        // Build the root VSTemplate DTO.
        VSTemplate vsTemplate = new()
        {
            Version = "3.0.0",
            Type = "Project",
            TemplateData = new TemplateData
            {
                Name = projectName,
                Description = "&lt;No description available&gt;",
                ProjectType = "CSharp",
                ProjectSubType = string.Empty,
                SortOrder = 1000,
                CreateNewFolder = true,
                DefaultName = projectName,
                ProvideDefaultName = true,
                LocationField = "Enabled",
                EnableLocationBrowseButton = true,
                Icon = "__TemplateIcon.ico"
            },
            TemplateContent = new TemplateContent
            {
                Project = BuildProject(stagingDirectory)
            },
            WizardExtension = new WizardExtension // Add the WizardExtension element for the post-processing wizard.
            {
                Assembly = "PostProcessingWizard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                FullClassName = "PostProcessingWizard.Wizard"
            }
        };

        return SerializeToXML(vsTemplate, false);
    }

    private static List<(string id, string version)> ReadProjectNuGetPackages(string csprojPath, bool isSdkStyle, BuildContext context)
    {
        List<(string id, string version)> packages = [];

        try
        {
            XDocument doc = XDocument.Load(csprojPath);
            XNamespace csprojNamespace = doc.Root?.GetDefaultNamespace() ?? XNamespace.None;

            if (isSdkStyle)
            {
                // SDK-style: <PackageReference Include="..." Version="..." />
                foreach (XElement pkg in doc.Descendants("PackageReference"))
                {
                    string? id = pkg.Attribute("Include")?.Value;
                    string? version = pkg.Attribute("Version")?.Value ?? pkg.Element("Version")?.Value;
                    if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(version))
                    {
                        packages.Add((id, version));
                    }
                }
            }
            else
            {
                // Legacy: <Reference Include="..."><HintPath>packages\PackageId.Version\lib\...</HintPath></Reference>
                foreach (XElement reference in doc.Descendants(csprojNamespace + "Reference"))
                {
                    XElement? hintPath = reference.Element(csprojNamespace + "HintPath");
                    if (hintPath != null)
                    {
                        string path = hintPath.Value.Replace('/', '\\');
                        int packagesIdx = path.IndexOf("packages\\", StringComparison.OrdinalIgnoreCase);
                        if (packagesIdx >= 0)
                        {
                            // Example: packages\Newtonsoft.Json.12.0.3\lib\net45\Newtonsoft.Json.dll
                            string[] parts = path[(packagesIdx + 9)..].Split('\\');
                            if (parts.Length > 0)
                            {
                                string idAndVersion = parts[0]; // e.g., Newtonsoft.Json.12.0.3
                                // Extract ID and version.
                                StringBuilder versionBuilder = new();
                                int i = idAndVersion.Length - 1;
                                bool foundDot = false;

                                while (i >= 0)
                                {
                                    char c = idAndVersion[i];
                                    if (char.IsDigit(c) || c == '.')
                                    {
                                        versionBuilder.Insert(0, c);
                                        if (c == '.')
                                        {
                                            foundDot = true;
                                        }
                                        i--;
                                    }
                                    else
                                    {
                                        // Optionally allow prerelease labels (e.g., -beta).
                                        if (c == '-' && foundDot && i < idAndVersion.Length - 1 && char.IsLetterOrDigit(idAndVersion[i + 1]))
                                        {
                                            versionBuilder.Insert(0, c);
                                            i--;
                                            // Continue collecting prerelease label.
                                            while (i >= 0 && (char.IsLetterOrDigit(idAndVersion[i]) || idAndVersion[i] == '.' || idAndVersion[i] == '-'))
                                            {
                                                versionBuilder.Insert(0, idAndVersion[i]);
                                                i--;
                                            }
                                        }
                                        break;
                                    }
                                }

                                // Remove trailing dot if present
                                string version = versionBuilder.ToString().Trim('.');
                                string id = idAndVersion[..(i + 1)];

                                if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(version))
                                {
                                    packages.Add((id, version));
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            context.Log.Error($"Could not read NuGet packages for {csprojPath}: {ex.Message}");
        }

        return packages;
    }

    private static string SerializeToXML<T>(T record, bool includeXMLDeclaration = true)
    {
        string xml = string.Empty;
        XmlSerializerNamespaces xmlNamespace = new();
        xmlNamespace.Add("", "http://schemas.microsoft.com/developer/vstemplate/2005");
        XmlSerializer serializer = new(typeof(T));

        using StringWriter stringWriter = new();

        if (includeXMLDeclaration)
        {
            serializer.Serialize(stringWriter, record, xmlNamespace);
            xml = stringWriter.ToString();
        }
        else
        {
            XmlWriterSettings settings = new()
            {
                OmitXmlDeclaration = true,
                Indent = true,
                Encoding = new UTF8Encoding(false)
            };

            using XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings);
            serializer.Serialize(xmlWriter, record, xmlNamespace);
            xml = stringWriter.ToString();
        }

        return xml;
    }

    private static void ReplaceProjectGuidWithTemplateParameter(string csprojPath, bool isSdkStyle)
    {
        if (isSdkStyle)
        {
            return; // SDK-style projects do not rely on a ProjectGuid element.
        }

        XDocument doc = XDocument.Load(csprojPath);

        // Find the ProjectGuid element regardless of namespace
        XElement? guidElement = doc.Descendants()
            .FirstOrDefault(e => e.Name.LocalName == "ProjectGuid");

        if (guidElement != null)
        {
            guidElement.Value = "{$guid1$}";
            doc.Save(csprojPath);
        }
    }

    private static void CapitalizeFirstLevelFolders(string directoryPath)
    {
        foreach (string folder in Directory.GetDirectories(directoryPath))
        {
            string originalFolderName = Path.GetFileName(folder);

            RenameFolder(directoryPath, originalFolderName, originalFolderName.ToUpperInvariant());
        }
    }

    private static void RenameFolder(string directoryPath, string oldName, string newName)
    {
        string fullPathToOld = Path.Combine(directoryPath, oldName);
        if (string.Equals(oldName, newName, StringComparison.Ordinal) || !Directory.Exists(fullPathToOld))
        {
            return;
        }

        string fullPathToNew = Path.Combine(directoryPath, newName);

        // Always use a temp name to force the rename, even if only case is changing.
        string tempDir = Path.Combine(directoryPath, Guid.NewGuid().ToString("N"));
        Directory.Move(fullPathToOld, tempDir);

        // If a folder with the target name exists, delete it first.
        if (Directory.Exists(fullPathToNew))
        {
            Directory.Delete(fullPathToNew, recursive: true);
        }

        Directory.Move(tempDir, fullPathToNew);
    }

    private static void CompressDirectory(string directoryPathToCompress)
    {
        if (!Directory.Exists(directoryPathToCompress))
        {
            throw new DirectoryNotFoundException($"Directory '{directoryPathToCompress}' does not exist.");
        }

        string zipPath = Path.Combine(Path.GetDirectoryName(directoryPathToCompress)!, Path.GetFileName(directoryPathToCompress) + ".zip");
        string tempZipPath = zipPath + ".tmp";
        ZipFile.CreateFromDirectory(directoryPathToCompress, tempZipPath);
        File.Move(tempZipPath, zipPath, overwrite: true);

        Directory.Delete(directoryPathToCompress, recursive: true);
    }
}
