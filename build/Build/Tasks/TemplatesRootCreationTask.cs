using Build.DTOs;
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

        foreach (TemplateProject templateProject in context.TemplateProjects)
        {
            if (!templateProject.IsApplication)
            {
                continue;
            }

            // Create staging folder for root template for the application.
            string templateRootDir = Path.Combine(templateProject.OutputDirectoryPathAbsolute, templateProject.Name + "_Template");
            Directory.CreateDirectory(templateRootDir);

            // Move the application's template zip to the root template directory.
            string appTemplateZipPath = Path.Combine(templateProject.OutputDirectoryPathAbsolute, templateProject.Name + ".zip");
            if (!File.Exists(appTemplateZipPath))
            {
                throw new FileNotFoundException($"Template zip file not found for project '{templateProject.Name}' at '{appTemplateZipPath}'.");
            }
            string destinationAppTemplateZipPath = Path.Combine(templateRootDir, templateProject.Name + ".zip");
            File.Move(appTemplateZipPath, destinationAppTemplateZipPath, overwrite: true);

            // Copy template for the app's referenced projects.
            string sourceDir = Path.Combine(context.AbsolutePathToRepo, "src");
            CopyReferencedProjectTemplatesToRoot(templateProject, sourceDir, templateRootDir, context);

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
            WriteRootVSTemplate(templateProject, templateRootDir);

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

    private static void CopyReferencedProjectTemplatesToRoot(TemplateProject templateProject, string sourceDir, string templateRootDir, BuildContext context)
    {
        foreach (string referencedProjectName in templateProject.ProjectNamesReferenced)
        {
            // Find the .csproj file for the referenced project.
            string[] csprojFiles = Directory.GetFiles(sourceDir, referencedProjectName + ".csproj", SearchOption.AllDirectories);
            if (csprojFiles.Length == 0)
            {
                throw new FileNotFoundException($"Could not find .csproj for referenced project '{referencedProjectName}' in '{sourceDir}'.");
            }

            string referencedCsprojPath = csprojFiles[0];
            TemplateProject? referencedTemplateProject = context.TemplateProjects
                .Where(tp => string.Equals(tp.CsprojFilePathAbsolute, referencedCsprojPath))
                .FirstOrDefault() ?? throw new InvalidOperationException($"Referenced project '{referencedProjectName}' at '{referencedCsprojPath}' is not listed as a template project.");

            string referencedProjectZipFileName = referencedProjectName + ".zip";
            string referencedZipPath = Path.Combine(referencedTemplateProject.OutputDirectoryPathAbsolute, referencedProjectZipFileName);
            if (!File.Exists(referencedZipPath))
            {
                throw new FileNotFoundException($"Template zip file not found for referenced project '{referencedProjectName}' at '{referencedZipPath}'.");
            }

            string destZipPath = Path.Combine(templateRootDir, referencedProjectZipFileName);
            File.Copy(referencedZipPath, destZipPath, overwrite: true);
        }
    }

    private static void WriteRootVSTemplate(TemplateProject templateProject, string templateRootDir)
    {
        // Find all template folders.
        List<string> allTemplateDirs = [.. Directory.GetDirectories(templateRootDir)
            .Where(d => File.Exists(Path.Combine(d, "MyTemplate.vstemplate")))];

        string appTemplateDir = Path.Combine(templateRootDir, templateProject.Name);
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

            bool isAppTemplate = string.Equals(folderName, appTemplateFolderName, StringComparison.OrdinalIgnoreCase);

            projectLinks.Add(new DTOs.ProjectTemplateLink
            {
                ProjectName = isAppTemplate ? TemplatesDefaultExportTask.GetAppProjectReplacementParameter() : projectNameAttr,
                CopyParameters = isAppTemplate,
                Value = folderName + @"\" + "MyTemplate.vstemplate"
            });
        }

        // Build the VSTemplate DTO
        var vsTemplate = new DTOs.VSTemplate
        {
            Version = "3.0.0",
            Type = "ProjectGroup",
            TemplateData = new DTOs.TemplateData
            {
                Name = templateProject.FriendlyName.Replace("Win", "Windows ") + " Solution Template",
                Description = templateProject.Description,
                DefaultName = templateProject.Name + "_Solution",
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
}
