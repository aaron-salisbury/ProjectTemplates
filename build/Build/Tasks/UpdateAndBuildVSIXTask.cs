using Build.DTOs;
using Cake.Common.Tools.MSBuild;
using Cake.Common.Tools.VSWhere;
using Cake.Common.Tools.VSWhere.Latest;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using static Build.BuildContext;

namespace Build.Tasks;

[TaskName("Update and Build VSIX")]
[IsDependentOn(typeof(TemplatesRootCreationTask))]
[TaskDescription("Update VSIX manifest with solution templates and template NuGet packages.")]
public sealed class UpdateAndBuildVSIXTask : FrostingTask<BuildContext>
{
    private const string VSIX_SOLUTION_NAME = "ProjectTemplatesVSIX";
    private const string VSIX_PROJECT_NAME = "ProjectTemplates";
    private const string VS_TEMPLATE_FOLDER_NAME = "VSTemplates";
    private const string TEMPLATE_PACKAGES_FOLDER_NAME = "TemplatePackages";

    public override bool ShouldRun(BuildContext context)
    {
        return context.Config == BuildConfigurations.Release;
    }

    public override void Run(BuildContext context)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        context.Log.Information($"Finalizing VSIX project...");

        string solutionDir = Path.Combine(context.SourceDirectory, VSIX_SOLUTION_NAME);
        string projectDir = Path.Combine(solutionDir, VSIX_PROJECT_NAME);
        string vsTemplateDir = Path.Combine(projectDir, VS_TEMPLATE_FOLDER_NAME);
        string packagesDir = Path.Combine(projectDir, TEMPLATE_PACKAGES_FOLDER_NAME);
        string csprojPath = Path.Combine(projectDir, $"{VSIX_PROJECT_NAME}.csproj");

        CleanImportLocations(vsTemplateDir, packagesDir);

        ImportExtensionIcon(projectDir, context);

        List<string> templateFileNames = ImportVSTemplates(vsTemplateDir, context);

        List<string> nupkgFileNames = ImportTemplateNuGetPackages(context.SourceDirectory, packagesDir);

        UpdateVSIXProjectFile(csprojPath, templateFileNames, nupkgFileNames);

        UpdateVSIXManifestFile(projectDir, templateFileNames, nupkgFileNames);

        BuildVSIXProject(context, csprojPath);

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
        foreach (string file in Directory.GetFiles(packagesDir, "*.nupkg", SearchOption.TopDirectoryOnly))
        {
            File.Delete(file);
        }
    }

    private static void ImportExtensionIcon(string projectDir, BuildContext context)
    {
        // Copy default template icon to staging directory.
        string contentDir = Path.Combine(context.AbsolutePathToRepo, "content");

        string iconSourcePath = Path.Combine(contentDir, "extension-icon.ico");
        if (File.Exists(iconSourcePath))
        {
            string iconDestinationPath = Path.Combine(projectDir, Path.GetFileName(iconSourcePath));
            File.Copy(iconSourcePath, iconDestinationPath, overwrite: true);
        }
        else
        {
            context.Log.Error($"VS extension icon file not found at {iconSourcePath}");
        }

        string imgSourcePath = Path.Combine(contentDir, "extension-icon.png");
        if (File.Exists(imgSourcePath))
        {
            string imgDestinationPath = Path.Combine(projectDir, Path.GetFileName(imgSourcePath));
            File.Copy(imgSourcePath, imgDestinationPath, overwrite: true);
        }
        else
        {
            context.Log.Error($"VS extension image file not found at {imgSourcePath}");
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
        // Distinctly copy each nupkg file used by template projects into the VSIX Packages folder.

        HashSet<string> nupkgFileNames = [];

        string templatesSourceDir = Path.Combine(sourceDir, "TemplateSources");
        string templatePackagesDir = Path.Combine(templatesSourceDir, "packages");
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
            .FirstOrDefault(g => (string?)g.Attribute("Label") == VS_TEMPLATE_FOLDER_NAME);
        bool templatesItemGroupIsNew = false;
        if (templatesItemGroup == null)
        {
            templatesItemGroup = new XElement(ns + "ItemGroup", new XAttribute("Label", VS_TEMPLATE_FOLDER_NAME));
            templatesItemGroupIsNew = true;
        }

        // Remove all <Content> elements for VS templates.
        templatesItemGroup.Elements(ns + "Content")
            .Where(e => ((string?)e.Attribute("Include"))?.StartsWith($"{VS_TEMPLATE_FOLDER_NAME}\\") == true)
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

        // --- EXTENSION ICON PNG ---
        // Ensure extension-icon.png has IncludeInVSIX=true
        XElement? pngIconElement = doc.Descendants(ns + "Content")
            .FirstOrDefault(e => (string?)e.Attribute("Include") == "extension-icon.png");

        if (pngIconElement != null)
        {
            // Check if IncludeInVSIX element exists
            XElement? includeInVsix = pngIconElement.Element(ns + "IncludeInVSIX");
            if (includeInVsix == null)
            {
                pngIconElement.Add(new XElement(ns + "IncludeInVSIX", "true"));
            }
            else
            {
                includeInVsix.Value = "true";
            }
        }

        // --- TEMPLATE PACKAGES ---
        // Find or create the ItemGroup with Label="TemplatePackages".
        XElement? packagesItemGroup = doc.Descendants(ns + "ItemGroup")
            .FirstOrDefault(g => (string?)g.Attribute("Label") == TEMPLATE_PACKAGES_FOLDER_NAME);
        bool packagesItemGroupIsNew = false;
        if (packagesItemGroup == null)
        {
            packagesItemGroup = new XElement(ns + "ItemGroup", new XAttribute("Label", TEMPLATE_PACKAGES_FOLDER_NAME));
            packagesItemGroupIsNew = true;
        }

        // Remove all <None> elements for template packages.
        packagesItemGroup.Elements(ns + "None")
            .Where(e => ((string?)e.Attribute("Include"))?.StartsWith($"{TEMPLATE_PACKAGES_FOLDER_NAME}\\") == true)
            .ToList()
            .ForEach(e => e.Remove());

        // Add new <None> elements with IncludeInVSIX=true
        foreach (string nupkgFile in nupkgFileNames)
        {
            XElement noneElement = new(ns + "None",
                new XAttribute("Include", $"{TEMPLATE_PACKAGES_FOLDER_NAME}\\{nupkgFile}"),
                new XElement(ns + "CopyToOutputDirectory", "Always"),
                new XElement(ns + "IncludeInVSIX", "true")
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

        XDocument doc = XDocument.Load(manifestPath);
        XNamespace ns = "http://schemas.microsoft.com/developer/vsx-schema/2011";
        XNamespace d = "http://schemas.microsoft.com/developer/vsx-schema-design/2011";
        XElement? assetsNode = doc.Descendants(ns + "Assets").FirstOrDefault() ?? throw new InvalidOperationException("No <Assets> node found in the VSIX manifest.");

        // Remove old template and package asset entries.
        assetsNode.Elements(ns + "Asset")
            .Where(e =>
            {
                string? typeAttr = (string?)e.Attribute("Type");
                return typeAttr != null &&
                       (typeAttr.EndsWith(".ProjectTemplate", StringComparison.OrdinalIgnoreCase) ||
                        typeAttr.EndsWith(".nupkg", StringComparison.OrdinalIgnoreCase));
            })
            .ToList()
            .ForEach(e => e.Remove());

        // Add new package asset elements.
        foreach (string nupkgFileName in nupkgFileNames)
        {
            XElement asset = new(ns + "Asset",
                new XAttribute(d + "Source", "File"),
                new XAttribute(d + "VsixSubPath", TEMPLATE_PACKAGES_FOLDER_NAME),
                new XAttribute("Path", $"{TEMPLATE_PACKAGES_FOLDER_NAME}\\{nupkgFileName}"),
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
                new XAttribute("Path", VS_TEMPLATE_FOLDER_NAME),
                new XAttribute(d + "TargetPath", $"{VS_TEMPLATE_FOLDER_NAME}\\{templateFileName}")
            );
            assetsNode.Add(asset);
        }

        doc.Save(manifestPath);
    }

    private static void BuildVSIXProject(BuildContext context, string vsixCsprojPath)
    {
        Cake.Core.IO.DirectoryPath vsInstallDir = context.VSWhereLatest(new VSWhereLatestSettings
        {
            Requires = "Microsoft.VisualStudio.Component.VSSDK"
        });

        // VS 2026 uses v18.0 for MSBuild tooling paths
        string vsToolsPath = Path.Combine(vsInstallDir.FullPath, "MSBuild", "Microsoft", "VisualStudio", "v18.0");

        context.Log.Information($"Using VSToolsPath: {vsToolsPath}");

        // Verify the path exists
        if (!Directory.Exists(vsToolsPath))
        {
            throw new DirectoryNotFoundException($"VSToolsPath not found: {vsToolsPath}. Ensure Visual Studio 2026 with VSSDK is installed.");
        }

        // Use Visual Studio's MSBuild, not the .NET SDK MSBuild
        string msbuildPath = Path.Combine(vsInstallDir.FullPath, "MSBuild", "Current", "Bin", "MSBuild.exe");

        context.MSBuild(vsixCsprojPath, new MSBuildSettings
        {
            Targets = { "Restore", "Build" },
            Configuration = context.Config.ToString(),
            Verbosity = Verbosity.Minimal,
            ToolPath = new Cake.Core.IO.FilePath(msbuildPath),
            Properties =
            {
                { "VSToolsPath", new[] { vsToolsPath } },
                { "DeployExtension", new[] { "false" } },
                { "VisualStudioVersion", new[] { "18.0" } }
            }
        });
    }
}
