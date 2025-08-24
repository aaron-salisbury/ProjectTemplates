using Build.DTOs;
using Cake.Common.Tools.MSBuild;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Build.Tasks;

[TaskName("Update and Build VSIX")]
[IsDependentOn(typeof(TemplatesRootCreationTask))]
[TaskDescription("Update VSIX manifest with solution templates and template NuGet packages.")]
public sealed class UpdateAndBuildVSIXTask : FrostingTask<BuildContext>
{
    private const string VSIX_SOLUTION_NAME = "ProjectTemplatesVSIX";
    private const string VSIX_PROJECT_NAME = "ProjectTemplates";
    private const string VS_TEMPLATE_FOLDER_NAME = "VSTemplates";

    public override bool ShouldRun(BuildContext context)
    {
        return true;
        //return context.Config == BuildConfigurations.Release;
    }

    public override void Run(BuildContext context)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        context.Log.Information($"Finalizing VSIX project...");

        string sourceDir = Path.Combine(context.AbsolutePathToRepo, "src");
        string solutionDir = Path.Combine(sourceDir, VSIX_SOLUTION_NAME);
        string projectDir = Path.Combine(solutionDir, VSIX_PROJECT_NAME);
        string vsTemplateDir = Path.Combine(projectDir, VS_TEMPLATE_FOLDER_NAME);
        string packagesDir = Path.Combine(projectDir, "Packages");
        string csprojPath = Path.Combine(projectDir, VSIX_PROJECT_NAME + ".csproj");

        CleanImportLocations(vsTemplateDir, packagesDir);

        List<string> templateFileNames = ImportVSTemplates(vsTemplateDir, context);

        List<string> nupkgFileNames = ImportTemplateNuGetPackages(sourceDir, packagesDir);

        UpdateVSIXProjectFile(projectDir, templateFileNames, nupkgFileNames);

        UpdateVSIXManifestFile(projectDir, templateFileNames, nupkgFileNames);

        context.MSBuild(csprojPath, new MSBuildSettings
        {
            Target = "Build",
            Configuration = context.Config.ToString(),
            Verbosity = Verbosity.Minimal
        });

        stopwatch.Stop();
        double completionTime = Math.Round(stopwatch.Elapsed.TotalSeconds, 1);
        context.Log.Information($"Finalization of VSIX project complete ({completionTime}s)");
    }

    private static void CleanImportLocations(string vsTemplateDir, string packagesDir)
    {
        Directory.CreateDirectory(vsTemplateDir);
        foreach (string zipFile in Directory.GetFiles(vsTemplateDir, "*.zip", SearchOption.TopDirectoryOnly))
        {
            File.Delete(zipFile);
        }

        Directory.CreateDirectory(packagesDir);
        foreach (string file in Directory.GetFiles(packagesDir, "*", SearchOption.AllDirectories))
        {
            File.Delete(file);
        }
    }

    private static List<string> ImportVSTemplates(string vsTemplateDir, BuildContext context)
    {
        // Copy root template ZIPs into the VSIX template folder, keeping track of the file names.

        List<string> templateFileNames = [];

        foreach (TemplateProject templateProject in context.TemplateProjects)
        {
            if (!templateProject.IsApplication)
            {
                continue;
            }

            string rootTemplateZipPath = Path.Combine(templateProject.OutputDirectoryPathAbsolute, templateProject.Name + "_Template.zip");
            if (!File.Exists(rootTemplateZipPath))
            {
                throw new FileNotFoundException($"Template zip file not found: {rootTemplateZipPath}");
            }

            string destZipPath = Path.Combine(vsTemplateDir, Path.GetFileName(rootTemplateZipPath));
            File.Copy(rootTemplateZipPath, destZipPath, overwrite: true);
            templateFileNames.Add(Path.GetFileName(rootTemplateZipPath));
        }

        return templateFileNames;
    }

    private static List<string> ImportTemplateNuGetPackages(string sourceDir, string packagesDir)
    {
        // Distinctly copy each nupkg file that were in the WizardData into the VSIX Packages folder.

        HashSet<string> nupkgFileNames = [];

        string templatesSourceDir = Path.Combine(sourceDir, "TemplateSources");
        string templatePackagesDir = Path.Combine(templatesSourceDir, "TemplateSources");
        if (!Directory.Exists(templatePackagesDir))
        {
            throw new DirectoryNotFoundException($"The template packages directory '{templatePackagesDir}' does not exist.");
        }

        foreach (string nupkgFile in Directory.GetFiles(templatePackagesDir, "*.nupkg", SearchOption.AllDirectories))
        {
            string destNupkgPath = Path.Combine(packagesDir, Path.GetFileName(nupkgFile));
            File.Copy(nupkgFile, destNupkgPath, overwrite: true);
            nupkgFileNames.Add(Path.GetFileName(nupkgFile));
        }

        return [.. nupkgFileNames];
    }

    private static void UpdateVSIXProjectFile(string csprojPath, List<string> templateFileNames, List<string> nupkgFileNames)
    {
        // Update VSIX .csproj to include new import files.

        if (!File.Exists(csprojPath))
        {
            throw new FileNotFoundException($"VSIX project file not found: {csprojPath}");
        }

        // Load the project XML.
        XDocument doc = XDocument.Load(csprojPath);
        XNamespace ns = doc.Root?.Name.Namespace ?? XNamespace.None;

        // --- VS TEMPLATES ---
        // Find or create the ItemGroup with Label="VSTemplates".
        XElement? templatesItemGroup = doc.Descendants(ns + "ItemGroup")
            .FirstOrDefault(g => (string?)g.Attribute("Label") == "VSTemplates");
        bool templatesItemGroupIsNew = false;
        if (templatesItemGroup == null)
        {
            templatesItemGroup = new XElement(ns + "ItemGroup", new XAttribute("Label", "VSTemplates"));
            templatesItemGroupIsNew = true;
        }

        // Remove all <Content> elements for VS templates.
        templatesItemGroup.Elements(ns + "Content")
            .Where(e => ((string?)e.Attribute("Include"))?.StartsWith(VS_TEMPLATE_FOLDER_NAME + "\\") == true)
            .ToList()
            .ForEach(e => e.Remove());

        // Add new <Content> elements.
        foreach (string fileName in templateFileNames)
        {
            XElement contentElement = new(ns + "Content",
                new XAttribute("Include", $"{VS_TEMPLATE_FOLDER_NAME}\\{fileName}"),
                new XElement(ns + "CopyToOutputDirectory", "Always"),
                new XElement(ns + "IncludeInVSIX", "true")
            );
            templatesItemGroup.Add(contentElement);
        }

        if (templatesItemGroupIsNew)
        {
            doc.Root?.Add(templatesItemGroup);
        }

        // --- TEMPLATE PACKAGES ---
        // Find or create the ItemGroup with Label="TemplatePackages".
        XElement? packagesItemGroup = doc.Descendants(ns + "ItemGroup")
            .FirstOrDefault(g => (string?)g.Attribute("Label") == "TemplatePackages");
        bool packagesItemGroupIsNew = false;
        if (packagesItemGroup == null)
        {
            packagesItemGroup = new XElement(ns + "ItemGroup", new XAttribute("Label", "TemplatePackages"));
            packagesItemGroupIsNew = true;
        }

        // Remove all <None> elements for template packages.
        packagesItemGroup.Elements(ns + "None")
            .Where(e => ((string?)e.Attribute("Include"))?.StartsWith("Packages\\") == true)
            .ToList()
            .ForEach(e => e.Remove());

        // Add new <None> elements.
        foreach (string nupkgFile in nupkgFileNames)
        {
            XElement noneElement = new(ns + "None",
                new XAttribute("Include", $"Packages\\{nupkgFile}"),
                new XElement(ns + "CopyToOutputDirectory", "Always")
            );
            packagesItemGroup.Add(noneElement);
        }

        if (packagesItemGroupIsNew)
        {
            doc.Root?.Add(packagesItemGroup);
        }

        doc.Save(csprojPath);
    }

    private static void UpdateVSIXManifestFile(string projectDir, List<string> templateFileNames, List<string> nupkgFileNames)
    {
        // Update VSIX manifest to include new import files.

        string manifestPath = Path.Combine(projectDir, "source.extension.vsixmanifest");
        if (!File.Exists(manifestPath))
        {
            throw new FileNotFoundException($"VSIX manifest file not found: {manifestPath}");
        }

        //TODO: Read the manifest doc, get the <Assets> node, remove old template and package entries, add new ones, and save.

        XDocument doc = XDocument.Load(manifestPath);
        XNamespace ns = "http://schemas.microsoft.com/developer/vsx-schema/2011";
        XNamespace d = "http://schemas.microsoft.com/developer/vsx-schema-design/2011";
        XElement? assetsNode = doc.Descendants(ns + "Assets").FirstOrDefault() ?? throw new InvalidOperationException("No <Assets> node found in the VSIX manifest.");

        // Remove old template and package asset entries.
        assetsNode.Elements(ns + "Asset")
            .Where(e =>
                (string?)e.Attribute(d + "VsixSubPath") == "ProjectTemplate" ||
                (string?)e.Attribute(d + "VsixSubPath") == "Packages")
            .ToList()
            .ForEach(e => e.Remove());

        // Add new package asset elements.
        foreach (string nupkgFileName in nupkgFileNames)
        {
            XElement asset = new(ns + "Asset",
                new XAttribute(d + "Source", "File"),
                new XAttribute(d + "VsixSubPath", "Packages"),
                new XAttribute("Path", $"Packages\\{nupkgFileName}"),
                new XAttribute("Type", nupkgFileName)
            );
            assetsNode.Add(asset);
        }

        // Add new template asset elements.
        foreach (string templateFileName in templateFileNames)
        {
            XElement asset = new(ns + "Asset",
                new XAttribute("Type", "Microsoft.VisualStudio.ProjectTemplate"),
                new XAttribute(d + "Source", "File"),
                new XAttribute("Path", "VSTemplates"),
                new XAttribute(d + "TargetPath", $"VSTemplates\\{templateFileName}")
            );
            assetsNode.Add(asset);
        }

        doc.Save(manifestPath);
    }
}
