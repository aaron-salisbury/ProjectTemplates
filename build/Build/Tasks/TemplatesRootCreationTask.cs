using Cake.Core.Diagnostics;
using Cake.Frosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Build.Tasks;

[TaskName("Build Root Templates")]
[IsDependentOn(typeof(TemplatesModificationTask))]
[TaskDescription("Combines individual project templates into root templates.")]
public sealed class TemplatesRootCreationTask : FrostingTask<BuildContext>
{
    public override bool ShouldRun(BuildContext context)
    {
        return true;
        //return context.Config == BuildConfigurations.Release;
    }

    public override void Run(BuildContext context)
    {
        // Ultimately, we want the user to be able to select one template to generate the entire solution project structure 
        // so we need to create a root template for each application project.

        Stopwatch stopwatch = Stopwatch.StartNew();
        context.Log.Information($"Building root templates...");

        string sourceDir = Path.Combine(context.AbsolutePathToRepo, "src");
        string[] allProjectFiles = Directory.GetFiles(sourceDir, "*.csproj", SearchOption.AllDirectories);

        foreach (string csprojPath in allProjectFiles)
        {
            if (!IsProjectAnApplication(csprojPath, context))
            {
                continue;
            }

            bool isSdkStyle = BuildContext.IsSdkStyleProject(csprojPath);
            string outputDir = BuildContext.DetermineAbsoluteOutputPath(csprojPath, isSdkStyle, context.Config, context);
            string projectName = Path.GetFileNameWithoutExtension(csprojPath);

            // Create staging folder for root template for the application.
            string templateRootDir = Path.Combine(outputDir, projectName + "_Template");
            Directory.CreateDirectory(templateRootDir);

            // Move the application's template zip to the root template directory.
            string appTemplateZipPath = Path.Combine(outputDir, projectName + ".zip");
            if (!File.Exists(appTemplateZipPath))
            {
                throw new FileNotFoundException($"Template zip file not found for project '{projectName}' at '{appTemplateZipPath}'.");
            }
            string destinationAppTemplateZipPath = Path.Combine(templateRootDir, projectName + ".zip");
            File.Move(appTemplateZipPath, destinationAppTemplateZipPath, overwrite: true);

            // Copy template for the app's referenced projects.
            Dictionary<string, string> fullProjectPathsByName = TemplatesModificationTask.GetFullProjectPathsByName(allProjectFiles);
            HashSet<string> referencedProjectNames = [.. TemplatesModificationTask.GetReferencedProjectNamesRecursive(csprojPath, fullProjectPathsByName, context)];
            CopyReferencedProjectTemplatesToRoot(referencedProjectNames, sourceDir, templateRootDir, context);

            // Unzip each template in the root template directory.
            foreach (string zipFile in Directory.GetFiles(templateRootDir, "*.zip"))
            {
                string extractDir = Path.Combine(templateRootDir, Path.GetFileNameWithoutExtension(zipFile));
                if (Directory.Exists(extractDir))
                {
                    Directory.Delete(extractDir, recursive: true);
                }
                System.IO.Compression.ZipFile.ExtractToDirectory(zipFile, extractDir);
                File.Delete(zipFile);
            }

            // Copy icon into the root template directory.
            string contentDir = Path.Combine(context.AbsolutePathToRepo, "content");
            string iconPath = Path.Combine(contentDir, "vs-extension-icon.png");
            string destIconPath = Path.Combine(templateRootDir, Path.GetFileName(iconPath));
            File.Copy(iconPath, destIconPath, overwrite: true);

            // Create Root.vstemplate file.
            WriteRootVSTemplate(templateRootDir, projectName, isSdkStyle);

            // Compress the root template directory into a zip file.
            string rootTemplateZipPath = templateRootDir + ".zip";
            if (File.Exists(rootTemplateZipPath))
            {
                File.Delete(rootTemplateZipPath);
            }
            System.IO.Compression.ZipFile.CreateFromDirectory(templateRootDir, rootTemplateZipPath);
            Directory.Delete(templateRootDir, recursive: true);
        }

        stopwatch.Stop();
        double completionTime = Math.Round(stopwatch.Elapsed.TotalSeconds, 1);
        context.Log.Information($"Creation of root templates complete ({completionTime}s)");
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

    private static void CopyReferencedProjectTemplatesToRoot(IEnumerable<string> referencedProjectNames, string sourceDir, string templateRootDir, BuildContext context)
    {
        foreach (string referencedProjectName in referencedProjectNames)
        {
            // Find the .csproj file for the referenced project.
            string[] csprojFiles = Directory.GetFiles(sourceDir, referencedProjectName + ".csproj", SearchOption.AllDirectories);
            if (csprojFiles.Length == 0)
            {
                throw new FileNotFoundException($"Could not find .csproj for referenced project '{referencedProjectName}' in '{sourceDir}'.");
            }

            string referencedCsprojPath = csprojFiles[0];
            bool isSdkStyle = BuildContext.IsSdkStyleProject(referencedCsprojPath);
            string referencedProjectOutputDir = BuildContext.DetermineAbsoluteOutputPath(referencedCsprojPath, isSdkStyle, context.Config, context);
            string referencedProjectZipFileName = referencedProjectName + ".zip";
            string referencedZipPath = Path.Combine(referencedProjectOutputDir, referencedProjectZipFileName);
            if (!File.Exists(referencedZipPath))
            {
                throw new FileNotFoundException($"Template zip file not found for referenced project '{referencedProjectName}' at '{referencedZipPath}'.");
            }

            string destZipPath = Path.Combine(templateRootDir, referencedProjectZipFileName);
            File.Copy(referencedZipPath, destZipPath, overwrite: true);
        }
    }

    private static void WriteRootVSTemplate(string templateRootDir, string projectName, bool isSdkStyle)
    {
        // Find all template folders.
        List<string> allTemplateDirs = [.. Directory.GetDirectories(templateRootDir)
            .Where(d => File.Exists(Path.Combine(d, "MyTemplate.vstemplate")))];

        string appTemplateDir = Path.Combine(templateRootDir, projectName);
        string appTemplateFolderName = Path.GetFileName(appTemplateDir);

        // Build ProjectTemplateLink DTOs
        List<DTOs.ProjectTemplateLink> projectLinks = [];
        foreach (string dir in allTemplateDirs)
        {
            // ProjectName: "$safeprojectname$." + everything after the first dot in the folder name.
            string folderName = Path.GetFileName(dir);
            int firstDot = folderName.IndexOf('.');
            string projectNameAttr = firstDot > 0
                ? $"$safeprojectname${folderName[firstDot..]}"
                : $"$safeprojectname$.{folderName}";

            projectLinks.Add(new DTOs.ProjectTemplateLink
            {
                ProjectName = projectNameAttr,
                CopyParameters = string.Equals(folderName, appTemplateFolderName, StringComparison.OrdinalIgnoreCase), //TODO: App project name should really end in ".Presentation" but I'll have to refactor a lot.
                Value = folderName + @"\" + "MyTemplate.vstemplate"
            });
        }

        // Determine name and description.
        (string name, string description) = GetProjectNameAndDescription(appTemplateDir, projectName, isSdkStyle);

        // Build the VSTemplate DTO
        var vsTemplate = new DTOs.VSTemplate
        {
            Version = "3.0.0",
            Type = "ProjectGroup",
            TemplateData = new DTOs.TemplateData
            {
                Name = name,
                Description = description,
                DefaultName = projectName + "_Solution",
                Icon = "vs-extension-icon.png",
                LanguageTag = "C#",
                PlatformTag = "Windows",
                ProjectTypeTag = "Desktop",
                ProjectType = "CSharp",
                ProjectSubType = "",
                SortOrder = 1000,
                CreateNewFolder = true,
                ProvideDefaultName = true,
                LocationField = "Enabled",
                EnableLocationBrowseButton = true
            },
            TemplateContent = new DTOs.TemplateContent
            {
                ProjectCollection = new DTOs.ProjectCollection
                {
                    ProjectTemplateLinks = projectLinks
                }
            }
        };

        // Serialize to XML.
        string xmlNamespace = "http://schemas.microsoft.com/developer/vstemplate/2005";
        XmlSerializerNamespaces ns = new();
        ns.Add("", xmlNamespace);
        XmlSerializer serializer = new(typeof(DTOs.VSTemplate));
        string vstemplatePath = Path.Combine(templateRootDir, "Root.vstemplate");
        using var fs = new FileStream(vstemplatePath, FileMode.Create, FileAccess.Write);
        using var writer = XmlWriter.Create(fs, new XmlWriterSettings { Indent = true, OmitXmlDeclaration = false });
        serializer.Serialize(writer, vsTemplate, ns);
    }

    private static (string name, string description) GetProjectNameAndDescription(string projectDirectory, string projectName, bool isSdkStyle)
    {
        string csprojPath = Path.Combine(projectDirectory, projectName + ".csproj");
        if (!File.Exists(csprojPath))
        {
            throw new FileNotFoundException("Could not find .csproj file to extract description.", csprojPath);
        }

        XDocument doc = XDocument.Load(csprojPath);
        XElement? descriptionElement = doc.Descendants("Description").FirstOrDefault();

        // Get description.
        string? description;
        if (descriptionElement != null && !string.IsNullOrWhiteSpace(descriptionElement.Value))
        {
            description = descriptionElement.Value.Trim();
        }
        else
        {
            throw new InvalidOperationException($"No description found in .csproj file for project '{projectName}' at '{csprojPath}'.");
        }

        // Get name.
        string? name;
        if (isSdkStyle)
        {
            XElement? productElement = doc.Descendants("Product").FirstOrDefault();
            if (string.IsNullOrWhiteSpace(productElement?.Value))
            {
                throw new InvalidOperationException($"Product element not found in SDK-style project '{projectName}' at '{csprojPath}'.");
            }

            name = productElement.Value.Trim();
        }
        else
        {
            XElement? assemblyNameElement = doc.Descendants("AssemblyName").FirstOrDefault();
            if (string.IsNullOrWhiteSpace(assemblyNameElement?.Value))
            {
                throw new InvalidOperationException($"AssemblyName element not found in .Net Framework project '{projectName}' at '{csprojPath}'.");
            }

            name = InsertSpacesInPascalCase(assemblyNameElement.Value.Trim());
        }

        return (name, description);

        // Helper: Insert spaces before capital letters (except the first).
        static string InsertSpacesInPascalCase(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            var sb = new System.Text.StringBuilder();
            sb.Append(input[0]);
            for (int i = 1; i < input.Length; i++)
            {
                if (char.IsUpper(input[i]) && !char.IsWhiteSpace(input[i - 1]))
                {
                    sb.Append(' ');
                }
                sb.Append(input[i]);
            }
            return sb.ToString();
        }
    }
}
