using Build.DTOs;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Build.Tasks;

[TaskName("Export Default Project Templates")]
[IsDependentOn(typeof(CompileProjectsTask))]
[TaskDescription("Attempts to mirror Visual Studio's 'Export Template Wizard' in generating a project template from every project in the source directory, besides the VS extension project.")]
public sealed class ExportDefaultProjectTemplatesTask : FrostingTask<BuildContext>
{
    public override bool ShouldRun(BuildContext context)
    {
        return true;
        //return context.Config == BuildConfigurations.Release;
    }

    public override void Run(BuildContext context)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        context.Log.Information($"Generating project templates...");

        // Gather all .csproj files in the repo.
        string contentDir = Path.Combine(context.AbsolutePathToRepo, "content");
        string sourceDir = Path.Combine(context.AbsolutePathToRepo, "src");
        string[] allProjectFiles = Directory.GetFiles(sourceDir, "*.csproj", SearchOption.AllDirectories);

        // Exclude projects being released on their own.
        HashSet<string> excludedPaths = context.ReleaseProjects.Select(rp => rp.FilePathAbsolute).ToHashSet(StringComparer.OrdinalIgnoreCase);
        List<string> projectsToTemplate = [.. allProjectFiles.Where(p => !excludedPaths.Contains(p))];

        // Generate a default template and compress it.
        foreach (string csprojPath in projectsToTemplate)
        {
            bool isSdkStyle = BuildContext.IsSdkStyleProject(csprojPath);
            string outputDir = BuildContext.DetermineAbsoluteOutputPath(csprojPath, isSdkStyle, context.Config, context);
            string projectName = Path.GetFileNameWithoutExtension(csprojPath);
            string templateStagingDir = Path.Combine(outputDir, projectName);

            if (ExportDefaultTemplate(csprojPath, templateStagingDir, isSdkStyle, context))
            {
                CapitalizeFirstLevelFolders(templateStagingDir);
                CompressDirectory(templateStagingDir);
            }
            else
            {
                context.Log.Error($"[FAIL] Failed to export default template for project: {csprojPath}");
            }
        }

        stopwatch.Stop();
        double completionTime = Math.Round(stopwatch.Elapsed.TotalSeconds, 1);
        context.Log.Information($"Generation of default project templates complete ({completionTime}s)");
    }

    internal static void CompressDirectory(string directoryPathToCompress)
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

    private static bool ExportDefaultTemplate(string csprojPath, string templateStagingDir, bool isSdkStyle, BuildContext context)
    {
        try
        {
            string projectDir = Path.GetDirectoryName(csprojPath)!;

            // Clean up any previous staging
            if (Directory.Exists(templateStagingDir))
            {
                Directory.Delete(templateStagingDir, recursive: true);
            }

            // Copy all files except bin/obj/.vs/.git
            CopyProjectFiles(projectDir, templateStagingDir);

            // Replace all usages of the project name in files with $safeprojectname$
            ApplySafeProjectNameToFiles(csprojPath, templateStagingDir);

            // Create the .vstemplate file.
            string vsTemplateXML = CreateVSTemplateContents(csprojPath, templateStagingDir, isSdkStyle, context);
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
            context.Log.Error($"[FAIL] Exception while staging template for {csprojPath}: {ex.Message}");
            return false;
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

    private static void CopyProjectFiles(string projectDirectory,string stagingDirectory)
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

    private static void ApplySafeProjectNameToFiles(string csprojPath, string stagingDirectory)
    {
        string fullProjectName = Path.GetFileNameWithoutExtension(csprojPath);

        // We want something like MyCoolApp.Presentation.Web to become $safeprojectname$.Presentation.Web
        int firstDot = fullProjectName.IndexOf('.');
        string projectNamePrefix = firstDot > 0 ? fullProjectName[..firstDot] : fullProjectName;

        foreach (string file in Directory.EnumerateFiles(stagingDirectory, "*", SearchOption.AllDirectories))
        {
            try
            {
                string content = File.ReadAllText(file);
                // Only do replacement if the file appears to be text.
                if (!content.Contains('\0')) // Crude check for binary.
                {
                    string replaced = content.Replace(projectNamePrefix, "$safeprojectname$", StringComparison.OrdinalIgnoreCase);
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

    private static HashSet<string> GetReferencedProjectNamesRecursive(string csprojPath, Dictionary<string, string> allProjectPathsByName, BuildContext context, HashSet<string>? discoveredNames = null)
    {
        discoveredNames ??= new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        try
        {
            XDocument doc = XDocument.Load(csprojPath);
            IEnumerable<string> projectReferences = doc.Descendants("ProjectReference")
                .Select(pr => pr.Attribute("Include")?.Value)
                .Where(path => !string.IsNullOrEmpty(path))
                .Select(path => Path.GetFullPath(Path.Combine(Path.GetDirectoryName(csprojPath)!, path!)));

            foreach (var referencedCsproj in projectReferences)
            {
                // Find the project name from the path.
                string? referencedName = allProjectPathsByName
                    .FirstOrDefault(kvp => string.Equals(kvp.Value, referencedCsproj, StringComparison.OrdinalIgnoreCase))
                    .Key;

                if (referencedName != null && discoveredNames.Add(referencedName))
                {
                    // Recurse into the referenced project.
                    GetReferencedProjectNamesRecursive(referencedCsproj, allProjectPathsByName, context, discoveredNames);
                }
            }
        }
        catch (Exception ex)
        {
            context.Log.Error($"Could not parse references for {csprojPath}: {ex.Message}");
        }

        return discoveredNames;
    }

    private static string ReadProjectDescription(string csprojPath, BuildContext context)
    {
        try
        {
            XDocument doc = XDocument.Load(csprojPath);
            XElement? descriptionElement = doc.Descendants("Description").FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(descriptionElement?.Value))
            {
                return descriptionElement.Value.Trim();
            }
        }
        catch (Exception ex)
        {
            context.Log.Error($"Could not read Description for {csprojPath}: {ex.Message}");
        }

        return "<No description available>";
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

    private static string CreateVSTemplateContents(string csprojPath, string stagingDirectory, bool isSdkStyle, BuildContext context)
    {
        string projectName = Path.GetFileNameWithoutExtension(csprojPath);
        string description = ReadProjectDescription(csprojPath, context);
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

                project.Items.Add(new DTOs.ProjectItem
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
                Description = description,
                ProjectType = "CSharp",
                ProjectSubType = "",
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
            }
        };

        return SerializeToXML(vsTemplate, false);
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
}
