using Build.DTOs;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Build.Tasks;

[TaskName("Generate Project Templates")]
[IsDependentOn(typeof(CompileProjectsTask))]
[TaskDescription("Generates project template from every project in the source directory, besides the VS extetnsion project.")]
public sealed class GenerateProjectTemplatesTask : FrostingTask<BuildContext>
{
    public override bool ShouldRun(BuildContext context)
    {
        return true;
        //return context.Config == BuildConfigurations.Release;
    }

    public override void Run(BuildContext context)
    {
        // ref: https://learn.microsoft.com/en-us/dotnet/core/tutorials/cli-templates-create-project-template

        Stopwatch stopwatch = Stopwatch.StartNew();
        context.Log.Information($"Generating project templates...");

        // Gather all .csproj files in the repo.
        string contentDir = Path.Combine(context.AbsolutePathToRepo, "content");
        string sourceDir = Path.Combine(context.AbsolutePathToRepo, "src");
        string vsixProductId = ReadVSIXManifestId(sourceDir);
        string[] allProjectFiles = Directory.GetFiles(sourceDir, "*.csproj", SearchOption.AllDirectories);

        // Exclude projects being released on their own.
        HashSet<string> excludedPaths = context.ReleaseProjects.Select(rp => rp.FilePathAbsolute).ToHashSet(StringComparer.OrdinalIgnoreCase);
        List<string> projectsToTemplate = [.. allProjectFiles.Where(p => !excludedPaths.Contains(p))];

        // Build a map of project name to csproj path for all projects.
        //Dictionary<string, string> allProjectPathsByName = projectsToTemplate.ToDictionary(
        //    path => Path.GetFileNameWithoutExtension(path),
        //    path => Path.GetFullPath(path),
        //    StringComparer.OrdinalIgnoreCase);

        // Generate a default template and copy it to the output directory.
        foreach (string csprojPath in projectsToTemplate)
        {
            bool isApplication = IsProjectAnApplication(csprojPath, context);
            bool isSdkStyle = BuildContext.IsSdkStyleProject(csprojPath);
            string outputDir = BuildContext.DetermineAbsoluteOutputPath(csprojPath, isSdkStyle, context.Config, context);
            string templateStagingDir = Path.Combine(outputDir, "ProjectTemplate");

            //TODO: Applications should be tracked and then round up child template to group together.
            //if (isApplication)
            //{
            //    HashSet<string> referencedNames = GetReferencedProjectNamesRecursive(csprojPath, allProjectPathsByName, context);
            //}

            if (!ExportDefaultTemplate(csprojPath, templateStagingDir, isSdkStyle, vsixProductId, context))
            {
                context.Log.Error($"[FAIL] Failed to export default template for project: {csprojPath}");
            }
        }

        stopwatch.Stop();
        double completionTime = Math.Round(stopwatch.Elapsed.TotalSeconds, 1);
        context.Log.Information($"Generation of project templates complete ({completionTime}s)");
    }

    private static bool ExportDefaultTemplate(string csprojPath, string templateStagingDir, bool isSdkStyle, string vsixProductId, BuildContext context)
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
            foreach (string file in Directory.EnumerateFiles(projectDir, "*", SearchOption.AllDirectories))
            {
                string relativePath = Path.GetRelativePath(projectDir, file);
                if (relativePath.StartsWith("bin") || relativePath.StartsWith("obj") ||
                    relativePath.StartsWith(".vs") || relativePath.StartsWith(".git"))
                {
                    continue;
                }

                string destFile = Path.Combine(templateStagingDir, relativePath);
                Directory.CreateDirectory(Path.GetDirectoryName(destFile)!);
                File.Copy(file, destFile, overwrite: true);
            }

            // Create the .vstemplate file.
            string vsTemplateXML = CreateVSTemplateContents(csprojPath, templateStagingDir, isSdkStyle, vsixProductId, context);
            string vstemplatePath = Path.Combine(templateStagingDir, "MyTemplate.vstemplate");
            File.WriteAllText(vstemplatePath, vsTemplateXML);

            // Copy default template icon to staging directory.
            string contentDir = Path.Combine(context.AbsolutePathToRepo, "content");
            string iconSourcePath = Path.Combine(contentDir, "__TemplateIcon.ico");
            string iconDestPath = Path.Combine(templateStagingDir, "__TemplateIcon.ico");
            if (File.Exists(iconSourcePath))
            {
                File.Copy(iconSourcePath, iconDestPath, overwrite: true);
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

    private static bool IsProjectAnApplication(string csprojPath, BuildContext context)
    {
        try
        {
            XDocument doc = XDocument.Load(csprojPath);
            XElement? outputTypeElement = doc.Descendants("OutputType").FirstOrDefault();

            if (outputTypeElement != null)
            {
                string value = outputTypeElement.Value.Trim();
                return value.Equals("Exe", StringComparison.OrdinalIgnoreCase) || value.Equals("WinExe", StringComparison.OrdinalIgnoreCase);
            }

            // If OutputType is missing, assume library (per SDK-style convention).
            return false;
        }
        catch (Exception ex)
        {
            context.Log.Error($"Could not determine OutputType for {csprojPath}: {ex.Message}");
            return false;
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

    private static string ReadVSIXManifestId(string rootDirectory)
    {
        string? vsixManifestPath = Directory.GetFiles(rootDirectory, "source.extension.vsixmanifest", SearchOption.AllDirectories).FirstOrDefault();

        string? productId = null;
        if (!string.IsNullOrEmpty(vsixManifestPath))
        {
            XDocument manifestDoc = XDocument.Load(vsixManifestPath);
            XElement? identityElement = manifestDoc.Descendants().FirstOrDefault(e => e.Name.LocalName == "Identity");
            
            if (identityElement != null)
            {
                XAttribute? idAttr = identityElement.Attribute("Id");
                if (idAttr != null)
                {
                    productId = idAttr.Value;
                }
            }
        }

        if (!string.IsNullOrEmpty(productId))
        {
            return productId;
        }
        else
        {
            throw new InvalidOperationException("Could not determine product ID from VSIX manifest.");
        }
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

    private static string CreateVSTemplateContents(string csprojPath, string stagingDirectory, bool isSdkStyle, string vsixProductId, BuildContext context)
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

        // Only add wizard elements for non-SDK-style projects.
        if (!isSdkStyle)
        {
            vsTemplate.WizardExtension = new WizardExtension
            {
                Assembly = "NuGet.VisualStudio.Interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                FullClassName = "NuGet.VisualStudio.TemplateWizard"
            };
            vsTemplate.WizardData = new WizardData
            {
                Packages = nugetPackages.Any()
                    ? new DTOs.Packages
                    {
                        repository = "extension",
                        repositoryId = vsixProductId,
                        PackageList = [.. nugetPackages.Select(pkg => new DTOs.Package { id = pkg.id, version = pkg.version })]
                    }
                    : null
            };
        }

        // Serialize to XML.
        XmlSerializerNamespaces xmlNamespace = new();
        xmlNamespace.Add("", "http://schemas.microsoft.com/developer/vstemplate/2005");
        XmlSerializer serializer = new(typeof(DTOs.VSTemplate));
        using StringWriter xmlWriter = new();
        serializer.Serialize(xmlWriter, vsTemplate, xmlNamespace);

        return xmlWriter.ToString();
    }
}
