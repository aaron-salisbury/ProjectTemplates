using Build.DTOs;
using Cake.Common.IO;
using Cake.Common.IO.Paths;
using Cake.Common.Xml;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Xml.Linq;

namespace Build;

public sealed class BuildContext : FrostingContext
{
    internal const string REPO_AND_SOLUTION_NAME = "ProjectTemplates";
    internal const string LOGO_SVG_FILENAME = "logo.svg";

    private static readonly string[] RELEASE_PROJECT_NAMES = ["ProjectTemplates"];

    public enum BuildConfigurations
    {
        Debug,
        Release
    }

    public BuildConfigurations Config { get; }
    public JsonSerializerOptions SerializerOptions { get; }
    public string AbsolutePathToRepo { get; }
    public List<ReleaseProject> ReleaseProjects { get; }

    public BuildContext(ICakeContext context) : base(context)
    {
        string configArgument = context.Arguments.GetArgument("Configuration") ?? string.Empty;
        Config = configArgument.ToLower() switch
        {
            "release" => BuildConfigurations.Release,
            _ => BuildConfigurations.Debug,
        };

        SerializerOptions = new() { PropertyNameCaseInsensitive = true };
        AbsolutePathToRepo = GetRepoAbsolutePath(REPO_AND_SOLUTION_NAME, this);
        ReleaseProjects = [.. RELEASE_PROJECT_NAMES.Select(name => CreateReleaseProject(this, name))];
    }

    private static ReleaseProject CreateReleaseProject(BuildContext context, string projectName)
    {
        string projectDirectoryPath = System.IO.Path.Combine(context.AbsolutePathToRepo, "src", projectName);
        string csprojPath = System.IO.Path.Combine(projectDirectoryPath, $"{projectName}.csproj");
        bool isSdkStyleProject = IsSdkStyleProject(csprojPath);

        return new ReleaseProject
        {
            Name = projectName,
            DirectoryPathAbsolute = projectDirectoryPath,
            FilePathAbsolute = csprojPath,
            OutputDirectoryPathAbsolute = DetermineAbsoluteOutputPath(csprojPath, isSdkStyleProject, context.Config, context),
            IsSdkStyleProject = isSdkStyleProject
        };
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

    internal static bool IsSdkStyleProject(string csprojPath)
    {
        XDocument doc = XDocument.Load(csprojPath);
        XElement? projectElement = doc.Root;

        return projectElement?.Attribute("Sdk") != null;
    }

    internal static string DetermineAbsoluteOutputPath(string csprojPath, bool isSdkStyleProject, BuildConfigurations config, ICakeContext context)
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
