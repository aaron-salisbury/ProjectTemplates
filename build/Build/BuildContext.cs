using Build.DTOs;
using Cake.Common.IO;
using Cake.Common.IO.Paths;
using Cake.Common.Xml;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Frosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Linq;

namespace Build;

public sealed class BuildContext : FrostingContext
{
    internal const string REPO_NAME = "ProjectTemplates";
    internal const string LOGO_SVG_FILENAME = "logo.svg";

    public enum BuildConfigurations
    {
        Debug,
        Release
    }

    public BuildConfigurations Config { get; }
    public JsonSerializerOptions SerializerOptions { get; }
    public string AbsolutePathToRepo { get; }
    public IEnumerable<TemplateProject> TemplateProjects { get; }

    public BuildContext(ICakeContext context) : base(context)
    {
        string configArgument = context.Arguments.GetArgument("Configuration") ?? string.Empty;
        Config = configArgument.ToLower() switch
        {
            "release" => BuildConfigurations.Release,
            _ => BuildConfigurations.Debug,
        };

        SerializerOptions = new() { PropertyNameCaseInsensitive = true };
        AbsolutePathToRepo = GetRepoAbsolutePath(REPO_NAME, this);
        TemplateProjects = GatherTemplateProjects(this);
    }

    private static IEnumerable<TemplateProject> GatherTemplateProjects(BuildContext context)
    {
        string sourceDir = System.IO.Path.Combine(context.AbsolutePathToRepo, "src");
        string templatesSourceDir = System.IO.Path.Combine(sourceDir, "TemplateSources"); // TemplateSources contains subfolders for generations of .NET SDK.
        string[] allTemplateProjectFiles = Directory.GetFiles(templatesSourceDir, "*.csproj", SearchOption.AllDirectories);
        Dictionary<string, string> fullProjectPathsByName = allTemplateProjectFiles.ToDictionary(
            path => System.IO.Path.GetFileNameWithoutExtension(path),
            path => System.IO.Path.GetFullPath(path),
            StringComparer.OrdinalIgnoreCase);

        foreach (string csprojPath in allTemplateProjectFiles)
        {
            bool isApplication = IsProjectAnApplication(csprojPath, context);
            bool isSdkStyleProject = BuildContext.IsSdkStyleProject(csprojPath);
            string projectName = System.IO.Path.GetFileNameWithoutExtension(csprojPath);
            (string friendlyName, string description) = GetProjectFriendlyNameAndDescription(csprojPath, projectName, isApplication, isSdkStyleProject);

            yield return new TemplateProject
            {
                Name = System.IO.Path.GetFileNameWithoutExtension(csprojPath),
                DirectoryPathAbsolute = System.IO.Path.GetDirectoryName(csprojPath)!,
                CsprojFilePathAbsolute = csprojPath,
                OutputDirectoryPathAbsolute = DetermineAbsoluteOutputPath(csprojPath, isSdkStyleProject, context.Config, context),
                IsSdkStyleProject = isSdkStyleProject,
                IsApplication = isApplication,
                FriendlyName = friendlyName,
                Description = description,
                ProjectNamesReferenced = [.. GetReferencedProjectNamesRecursive(csprojPath, fullProjectPathsByName, context)]
            };
        }
    }

    private static bool IsProjectAnApplication(string csprojPath, BuildContext context)
    {
        try
        {
            XDocument doc = XDocument.Load(csprojPath);
            XNamespace ns = doc.Root?.Name.Namespace ?? XNamespace.None;
            XElement? outputTypeElement = doc.Descendants(ns + "OutputType").FirstOrDefault();

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

    private static (string friendlyName, string description) GetProjectFriendlyNameAndDescription(string csprojPath, string projectName, bool isApplication, bool isSdkStyle)
    {
        if (!File.Exists(csprojPath))
        {
            throw new FileNotFoundException("Could not find .csproj file to extract description.", csprojPath);
        }

        XDocument doc = XDocument.Load(csprojPath);
        XNamespace ns = doc.Root?.Name.Namespace ?? XNamespace.None;
        XElement? descriptionElement = doc.Descendants(ns + "Description").FirstOrDefault();

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
        if (isApplication && isSdkStyle)
        {
            XElement? productElement = doc.Descendants(ns + "Product").FirstOrDefault();
            if (string.IsNullOrWhiteSpace(productElement?.Value))
            {
                throw new InvalidOperationException($"Product element not found in SDK-style project '{projectName}' at '{csprojPath}'.");
            }

            name = productElement.Value.Trim();
        }
        else
        {
            name = InsertSpacesInPascalCase(projectName);
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

    private static HashSet<string> GetReferencedProjectNamesRecursive(string csprojPath, Dictionary<string, string> fullProjectPathsByName, BuildContext context, HashSet<string>? discoveredNames = null)
    {
        discoveredNames ??= new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        try
        {
            XDocument doc = XDocument.Load(csprojPath);
            IEnumerable<string> projectReferences = doc.Descendants()
                .Where(e => e.Name.LocalName == "ProjectReference")
                .Select(pr => pr.Attribute("Include")?.Value)
                .Where(path => !string.IsNullOrEmpty(path))
                .Select(path => System.IO.Path.GetFullPath(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(csprojPath)!, path!)));

            foreach (var referencedCsproj in projectReferences)
            {
                string referencedName = System.IO.Path.GetFileNameWithoutExtension(referencedCsproj);

                if (discoveredNames.Add(referencedName))
                {
                    if (fullProjectPathsByName.TryGetValue(referencedName, out var referencedCsprojPath))
                    {
                        GetReferencedProjectNamesRecursive(referencedCsprojPath, fullProjectPathsByName, context, discoveredNames);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            context.Log.Error($"Could not parse references for {csprojPath}: {ex.Message}");
        }

        return discoveredNames;
    }

    private static string GetRepoAbsolutePath(string repoName, ICakeContext context)
    {
        // Start from the working directory
        DirectoryPath dir = context.Environment.WorkingDirectory;

        // Traverse up until we find the directory named after the repository name.
        while (dir != null && !dir.GetDirectoryName().Equals(repoName, StringComparison.OrdinalIgnoreCase))
        {
            dir = dir.GetParent();
        }

        if (dir == null)
        {
            throw new InvalidOperationException($"Could not find repository root directory named '{repoName}' in parent chain.");
        }

        return dir.FullPath;
    }

    private static ConvertableDirectoryPath? GetConfiguredOutputPath(string csprojPath, BuildConfigurations config, ICakeContext context)
    {
        XDocument doc = XDocument.Load(csprojPath);
        XNamespace? ns = doc.Root?.Name.Namespace ?? XNamespace.None;
        string configString = config.ToString();

        // 1. Look for OutputPath in PropertyGroup with a matching Condition.
        foreach (XElement pg in doc.Descendants(ns + "PropertyGroup"))
        {
            string? condition = (string?)pg.Attribute("Condition");
            if (!string.IsNullOrWhiteSpace(condition) 
                && condition.Contains("'$(Configuration)'", StringComparison.OrdinalIgnoreCase) 
                && condition.Contains(configString, StringComparison.OrdinalIgnoreCase))
            {
                XElement? outputPathElem = pg.Element(ns + "OutputPath");
                if (outputPathElem != null && !string.IsNullOrWhiteSpace(outputPathElem.Value))
                {
                    var normalized = outputPathElem.Value.Replace('\\', '/').TrimEnd('/', '\\');
                    return context.Directory(normalized);
                }
            }
        }

        // 2. Fallback: Look for OutputPath in any PropertyGroup (global).
        foreach (XElement pg in doc.Descendants(ns + "PropertyGroup"))
        {
            XElement? outputPathElem = pg.Element(ns + "OutputPath");
            if (outputPathElem != null && !string.IsNullOrWhiteSpace(outputPathElem.Value))
            {
                var normalized = outputPathElem.Value.Replace('\\', '/').TrimEnd('/', '\\');
                return context.Directory(normalized);
            }
        }

        // 3. Not found / no override.
        return null;
    }

    private static string GetTargetFramework(string csprojPath, bool isSdkStyleProject, ICakeContext context)
    {
        if (isSdkStyleProject)
        {
            // Only supporting single TargetFramework for now.
            return context.XmlPeek(csprojPath, "/Project/PropertyGroup/TargetFramework");
        }

        return context.XmlPeek(csprojPath, "/Project/PropertyGroup/TargetFrameworkVersion");
    }

    private static bool IsSdkStyleProject(string csprojPath)
    {
        XDocument doc = XDocument.Load(csprojPath);
        XElement? projectElement = doc.Root;

        return projectElement?.Attribute("Sdk") != null;
    }

    private static string DetermineAbsoluteOutputPath(string csprojPath, bool isSdkStyleProject, BuildConfigurations config, ICakeContext context)
    {
        // Get the project root directory (directory containing the .csproj).
        var projectRoot = context.Directory(System.IO.Path.GetDirectoryName(csprojPath) ?? ".");

        // 1. Check for custom OutputPath.
        ConvertableDirectoryPath? customOutputPath = GetConfiguredOutputPath(csprojPath, config, context);
        if (customOutputPath != null)
        {
            // If the path is already absolute, return as is.
            if (System.IO.Path.IsPathRooted(customOutputPath.Path.FullPath))
            {
                return customOutputPath.Path.FullPath;
            }

            // Otherwise combine with project root.
            return (projectRoot + customOutputPath).Path.FullPath;
        }

        // 2. Default output path logic.
        string outputRelative;
        if (isSdkStyleProject)
        {
            string targetVersion = GetTargetFramework(csprojPath, true, context);
            outputRelative = $"bin/{config}/{targetVersion}";
        }
        else
        {
            outputRelative = $"bin/{config}";
        }

        return (projectRoot + context.Directory(outputRelative)).Path.FullPath;
    }
}
