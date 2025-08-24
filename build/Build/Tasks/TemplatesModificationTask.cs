using Build.DTOs;
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

namespace Build.Tasks;

[TaskName("Modify Project Templates")]
[IsDependentOn(typeof(TemplatesDefaultExportTask))]
[TaskDescription("Modify the generated default project templates.")]
public sealed class TemplatesModificationTask : FrostingTask<BuildContext>
{
    public override bool ShouldRun(BuildContext context)
    {
        return true;
        //return context.Config == BuildConfigurations.Release;
    }

    public override void Run(BuildContext context)
    {
        // The default generation process is designed for a single project only. We need to make edits in order to preserve NuGet packages and project references.

        Stopwatch stopwatch = Stopwatch.StartNew();
        context.Log.Information($"Modifying project templates...");

        foreach (TemplateProject templateProject in context.TemplateProjects)
        {
            string zipPath = Path.Combine(templateProject.OutputDirectoryPathAbsolute, templateProject.Name + ".zip");

            if (!File.Exists(zipPath))
            {
                continue;
            }

            // Unzip template to a staging directory.
            string stagingDir = Path.Combine(templateProject.OutputDirectoryPathAbsolute, templateProject.Name + "_staging");
            if (Directory.Exists(stagingDir))
            {
                Directory.Delete(stagingDir, recursive: true);
            }
            ZipFile.ExtractToDirectory(zipPath, stagingDir);

            // In .cs files, modify 'using' statements to referenced projects to use parameters.
            // Something to note is that application project names won't match their template's namespaces because for the final template, they've been suffixed with ".Presentation".
            // This should be fine though since they are at the top of the reference chain and won't be referenced by other projects.
            ApplySafeExternalUsings(stagingDir, templateProject.ProjectNamesReferenced, context);
            //TODO: It would be nice to group usings that start with $safeprojectname$ and $ext_safeprojectname$ together at the bottom of the usings block.

            // In the .csproj file:
            //  - Modify 'Include' attribute & child 'Name' element of ProjectReferences to referenced projects to use parameters.
            //  - If not an SDK-style project, update the path to any NuGet packages that later we will include in the VSIX project.
            string stagedCsprojPath = Path.Combine(stagingDir, Path.GetFileName(templateProject.CsprojFilePathAbsolute));
            ApplySafeExternalProjectReferences(stagedCsprojPath, templateProject.ProjectNamesReferenced, context);
            //StepUpNuGetPackageHintPaths(stagedCsprojPath, isSdkStyle); //TODO: Not sure if I actually need this; need to test.
            //TODO: Not sure what to do with the value of the Product element of SDK-style executable projects. It should be a friendly version of $safeprojectname$

            // In the .vstemplate file:
            //  - Update ProjectItem elements for files w/ markup namespaces (such as XAML or manifest) with ReplaceParameters parameter values of 'false' to 'true'. *NOTE - Export task already does this.*
            //  - If not an SDK-style project, add wizard elements.
            string vsixProductId = ReadVSIXManifestId(Path.Combine(context.AbsolutePathToRepo, "src"));
            string vstemplatePath = Path.Combine(stagingDir, "MyTemplate.vstemplate");
            AddWizardElementsToVSTemplate(templateProject.CsprojFilePathAbsolute, vsixProductId, vstemplatePath, templateProject.IsSdkStyleProject, context);

            // Re-zip the modified template.
            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }
            ZipFile.CreateFromDirectory(stagingDir, zipPath);
            if (Directory.Exists(stagingDir))
            {
                Directory.Delete(stagingDir, recursive: true);
            }
        }

        stopwatch.Stop();
        double completionTime = Math.Round(stopwatch.Elapsed.TotalSeconds, 1);
        context.Log.Information($"Modification of project templates complete ({completionTime}s)");
    }

    private static void ApplySafeExternalUsings(string stagingDirectory, HashSet<string> referencedProjectNames, BuildContext context)
    {
        // In .cs files, modify 'using' statements to referenced projects to use parameters.
        foreach (string csFile in Directory.GetFiles(stagingDirectory, "*.cs", SearchOption.AllDirectories))
        {
            string text = File.ReadAllText(csFile);
            bool changed = false;

            foreach (string referencedProjectPrefix in referencedProjectNames)
            {
                // Pattern: using ReferencedPrefix(.AnythingElse);
                string pattern = $@"using\s+{Regex.Escape(referencedProjectPrefix)}(\.[\w\.]*)?;";
                string newText = Regex.Replace(
                    text,
                    pattern,
                    m => $"using $ext_safeprojectname${m.Groups[1].Value};",
                    RegexOptions.None
                );

                if (!ReferenceEquals(newText, text))
                {
                    text = newText;
                    changed = true;
                }
            }

            if (changed)
            {
                File.WriteAllText(csFile, text);
            }
        }
    }

    private static void ApplySafeExternalProjectReferences(string csprojPath, HashSet<string> referencedProjectNames, BuildContext context)
    {
        XDocument doc = XDocument.Load(csprojPath);
        bool changed = false;

        foreach (var projectReference in doc.Descendants().Where(e => e.Name.LocalName == "ProjectReference"))
        {
            // Update Include attribute
            XAttribute? includeAttr = projectReference.Attribute("Include");
            if (includeAttr != null)
            {
                string originalInclude = includeAttr.Value;
                foreach (string referencedName in referencedProjectNames)
                {
                    int firstDot = referencedName.IndexOf('.');
                    string prefix = firstDot > 0 ? referencedName[..firstDot] : referencedName;
                    // Replace prefix after path separator or at start, before a dot
                    string pattern = $@"(^|[\\/]){Regex.Escape(prefix)}(?=\.)";
                    string replaced = Regex.Replace(
                        originalInclude,
                        pattern,
                        m => m.Groups[1].Value + "$ext_safeprojectname$",
                        RegexOptions.None
                    );
                    if (!ReferenceEquals(replaced, originalInclude))
                    {
                        includeAttr.Value = replaced;
                        changed = true;
                        break; // Only replace once per reference
                    }
                }
            }

            // Update <Name> child element
            XElement? nameElement = projectReference.Elements().FirstOrDefault(e => e.Name.LocalName == "Name");
            if (nameElement != null)
            {
                string originalName = nameElement.Value;
                foreach (string referencedName in referencedProjectNames)
                {
                    int firstDot = referencedName.IndexOf('.');
                    string prefix = firstDot > 0 ? referencedName[..firstDot] : referencedName;
                    // Only replace at start of string, before a dot
                    string pattern = $@"^{Regex.Escape(prefix)}(?=\.)";
                    string replaced = Regex.Replace(
                        originalName,
                        pattern,
                        "$ext_safeprojectname$",
                        RegexOptions.None
                    );
                    if (!ReferenceEquals(replaced, originalName))
                    {
                        nameElement.Value = replaced;
                        changed = true;
                        break;
                    }
                }
            }
        }

        if (changed)
        {
            doc.Save(csprojPath);
        }
    }

    private static void StepUpNuGetPackageHintPaths(string csprojPath, bool isSdkStyle)
    {
        if (isSdkStyle)
        {
            return;
        }

        XDocument doc = XDocument.Load(csprojPath);
        bool changed = false;

        foreach (XElement reference in doc.Descendants().Where(e => e.Name.LocalName == "Reference"))
        {
            XElement? hintPath = reference.Elements().FirstOrDefault(e => e.Name.LocalName == "HintPath");
            if (hintPath != null && hintPath.Value.Contains(@"packages\", StringComparison.OrdinalIgnoreCase))
            {
                string value = hintPath.Value;
                if (value.Contains("packages\\", StringComparison.OrdinalIgnoreCase) || value.Contains("packages/", StringComparison.OrdinalIgnoreCase))
                {
                    hintPath.Value = @"..\" + value;
                    changed = true;
                }
            }
        }

        if (changed)
        {
            doc.Save(csprojPath);
        }
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

    private static void AddWizardElementsToVSTemplate(string csprojPath, string vsixProductId, string vstemplatePath, bool isSdkStyle, BuildContext context)
    {
        if (isSdkStyle)
        {
            return;
        }

        IEnumerable<(string id, string version)> nugetPackages = ReadProjectNuGetPackages(csprojPath, isSdkStyle, context);
        if (!nugetPackages.Any())
        {
            return;
        }

        XDocument doc = XDocument.Load(vstemplatePath);

        WizardExtension wizardExtension = new()
        {
            Assembly = "NuGet.VisualStudio.Interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            FullClassName = "NuGet.VisualStudio.TemplateWizard"
        };

        WizardData wizardData = new()
        {
            Packages = new DTOs.Packages
            {
                repository = "extension",
                repositoryId = vsixProductId,
                PackageList = [.. nugetPackages.Select(pkg => new DTOs.Package { id = pkg.id, version = pkg.version })]
            }
        };

        // Serialize WizardExtension and WizardData to XElement.
        XElement wizardExtensionElement;
        XElement wizardDataElement;
        var xmlNamespace = "http://schemas.microsoft.com/developer/vstemplate/2005";
        var ns = new XmlSerializerNamespaces();
        ns.Add("", xmlNamespace);

        // Helper to serialize an object to XElement.
        static XElement SerializeToXElement<T>(T elementDTO)
        {
            var serializer = new XmlSerializer(typeof(T));
            using var ms = new MemoryStream();
            using (var writer = XmlWriter.Create(ms, new XmlWriterSettings { OmitXmlDeclaration = true }))
            {
                serializer.Serialize(writer, elementDTO);
            }
            ms.Position = 0;
            using var reader = new StreamReader(ms);
            return XElement.Parse(reader.ReadToEnd());
        }

        wizardExtensionElement = SerializeToXElement(wizardExtension);
        wizardDataElement = SerializeToXElement(wizardData);

        // Add to the root VSTemplate element.
        XElement? root = doc.Root;
        if (root != null)
        {
            root.Add(wizardExtensionElement);
            root.Add(wizardDataElement);
            doc.Save(vstemplatePath);
        }
    }

    private static List<(string id, string version)> ReadProjectNuGetPackages(string csprojPath, bool isSdkStyle, BuildContext context)
    {
        var packages = new List<(string id, string version)>();

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
                                        if (c == '-' && foundDot && i < idAndVersion.Length - 1 && char.IsLetterOrDigit(idAndVersion[i + 1]))
                                        {
                                            versionBuilder.Insert(0, c);
                                            i--;
                                            while (i >= 0 && (char.IsLetterOrDigit(idAndVersion[i]) || idAndVersion[i] == '.' || idAndVersion[i] == '-'))
                                            {
                                                versionBuilder.Insert(0, idAndVersion[i]);
                                                i--;
                                            }
                                        }
                                        break;
                                    }
                                }

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

        // Remove duplicates by id+version
        return packages.Distinct().ToList();
    }
}
