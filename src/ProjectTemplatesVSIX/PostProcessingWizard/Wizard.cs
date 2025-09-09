using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace PostProcessingWizard;

public class Wizard : IWizard
{
    private static readonly object _logLock = new();
    private static string _rootDirectory = null;

    private readonly bool _isDebug = true;
    private readonly string _extensionDirectory;

    public Wizard()
    {
        _extensionDirectory = _isDebug ? EnsureExtensionDirectoryExists() : null;
    }

    public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
    {
        // Log run data.
        Log($"-------------------------------------------------{Environment.NewLine}");
        Log($"Run Started. WizardRunKind: {runKind}");
        StringBuilder replacementsBuilder = new();
        foreach (KeyValuePair<string, string> kvp in replacementsDictionary)
        {
            replacementsBuilder.Append($"    {kvp.Key} = {kvp.Value}{Environment.NewLine}");
        }
        Log($"ReplacementDictionary:{Environment.NewLine}{replacementsBuilder}");
        StringBuilder customParamsBuilder = new();
        foreach (object param in customParams)
        {
            customParamsBuilder.Append($"    {param} ({param?.GetType()}){Environment.NewLine}");
        }
        Log($"CustomParams:{Environment.NewLine}{customParamsBuilder}");

        // Determine the root directory.
        if (string.IsNullOrEmpty(_rootDirectory))
        {
            if (runKind == WizardRunKind.AsMultiProject && replacementsDictionary.TryGetValue("$destinationdirectory$", out string destinationDirectory))
            {
                _rootDirectory = destinationDirectory.Replace('\\', Path.DirectorySeparatorChar);
            }
            else if (replacementsDictionary.TryGetValue("$solutiondirectory$", out string solutionDirectory))
            {
                _rootDirectory = solutionDirectory.Replace('\\', Path.DirectorySeparatorChar).TrimEnd(Path.DirectorySeparatorChar);
            }
        }

        try
        {
            SetPrettyProjectNameParam(replacementsDictionary);
        }
        catch (Exception ex)
        {
            Log($"Error in RunStarted: {ex}");
        }
    }

    public bool ShouldAddProjectItem(string filePath) => true;

    public void ProjectItemFinishedGenerating(ProjectItem projectItem) { }

    public void ProjectFinishedGenerating(Project project)
    {
        try
        {
            AlphabetizeUsingsInGeneratedFile(project);

            UpdatePackageHintPaths(project);
        }
        catch (Exception ex)
        {
            Log($"Error in ProjectFinishedGenerating: {ex}");
        }
    }

    public void BeforeOpeningFile(ProjectItem projectItem) { }

    public void RunFinished() { }

    private void SetPrettyProjectNameParam(Dictionary<string, string> replacementsDictionary)
    {
        if (replacementsDictionary.TryGetValue("$safeprojectname$", out string safeProjectName))
        {
            // Replace underscores with spaces
            string prettyName = safeProjectName.Replace('_', ' ');

            // Insert spaces for PascalCase, numbers, and acronyms
            prettyName = Regex.Replace(
                prettyName,
                @"(?<=[a-z0-9])(?=[A-Z])|(?<=[A-Z])(?=[A-Z][a-z])|(?<=[A-Za-z])(?=[0-9])|(?<=[0-9])(?=[A-Za-z])",
                " "
            );

            // Replace multiple spaces with a single space and trim
            prettyName = Regex.Replace(prettyName, @"\s+", " ").Trim();

            replacementsDictionary["$prettyprojectname$"] = prettyName;

            Log($"Set pretty project name: {replacementsDictionary["$prettyprojectname$"]}");
        }
        else
        {
            Log($"Failed to retrieve $safeprojectname$ parameter.");
        }
    }

    private void AlphabetizeUsingsInGeneratedFile(Project project)
    {
        if (project == null)
        {
            Log("AlphabetizeUsingsInGeneratedFile: Project is null.");
            return;
        }

        // Recursively collect all .cs files in the project
        List<string> csFiles = [];
        CollectCsFilesFromProjectItems(project.ProjectItems, csFiles);

        foreach (string fileName in csFiles)
        {
            try
            {
                if (!fileName.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                // Read the file contents.
                string fileContent = File.ReadAllText(fileName);

                // Find all using statements at the top of the file.
                string[] lines = fileContent.Split('\n');
                List<string> usingLines = [];
                List<string> otherLines = [];
                bool inUsingBlock = true;

                foreach (string line in lines)
                {
                    string trimmed = line.Trim();
                    if (inUsingBlock && trimmed.StartsWith("using ") && trimmed.EndsWith(";"))
                    {
                        usingLines.Add(line);
                    }
                    else
                    {
                        inUsingBlock = false;
                        otherLines.Add(line);
                    }
                }

                // Alphabetize the using statements
                usingLines.Sort((a, b) => string.Compare(a, b, StringComparison.Ordinal));

                // Reconstruct the file
                string newContent = string.Join("\n", usingLines) + (usingLines.Count > 0 ? "\n" : "") + string.Join("\n", otherLines);

                // Write the updated content back to the file
                File.WriteAllText(fileName, newContent);
            }
            catch (Exception ex)
            {
                Log($"Error alphabetizing usings for {fileName}: {ex}");
            }
        }

        Log($"Alphabetized usings for project {project.FileName}");
    }

    private void CollectCsFilesFromProjectItems(ProjectItems items, List<string> csFiles)
    {
        if (items == null)
        {
            return;
        }

        foreach (ProjectItem item in items)
        {
            try
            {
                // Check all files in the item
                for (short i = 1; i <= item.FileCount; i++)
                {
                    string fileName = item.FileNames[i];
                    if (fileName.EndsWith(".cs", StringComparison.OrdinalIgnoreCase) && File.Exists(fileName))
                    {
                        csFiles.Add(fileName);
                    }
                }

                // Recurse into subitems
                if (item.ProjectItems != null && item.ProjectItems.Count > 0)
                {
                    CollectCsFilesFromProjectItems(item.ProjectItems, csFiles);
                }
            }
            catch (Exception ex)
            {
                Log($"Error collecting files from ProjectItem: {ex}");
            }
        }
    }

    private void UpdatePackageHintPaths(Project project)
    {
        if (project == null)
        {
            return;
        }

        string csproj = project.FullName;
        if (string.IsNullOrEmpty(csproj) || !File.Exists(csproj))
        {
            return;
        }

        Log($"Attempting to update project package hint paths ...");

        XDocument xDoc = XDocument.Load(csproj);
        XElement projectElement = xDoc.Root;
        if (projectElement?.Attribute("Sdk") != null)
        {
            return;
        }

        // Find the directory containing the .sln file.
        string projectDir = Path.GetDirectoryName(csproj);
        string currentDir = projectDir;
        int stepsUp = 0;

        while (!string.IsNullOrEmpty(currentDir) && currentDir.StartsWith(_rootDirectory, StringComparison.OrdinalIgnoreCase))
        {
            if (Directory.EnumerateFiles(currentDir, "*.sln", SearchOption.TopDirectoryOnly).Any())
            {
                break;
            }

            DirectoryInfo parent = Directory.GetParent(currentDir);
            if (parent == null || parent.FullName == currentDir)
            {
                break;
            }

            currentDir = parent.FullName;
            stepsUp++;
        }

        Log($"Number of Steps Found: {stepsUp}");

        // Update XML.
        string csprojXml = File.ReadAllText(csproj);
        XmlDocument doc = new();
        doc.LoadXml(csprojXml);

        bool changed = false;
        XmlNamespaceManager namespaceManager = new(doc.NameTable);
        if (doc.DocumentElement == null)
        {
            Log($"ERROR: DocumentElement is unavailable for {csproj}");
            return;
        }
        if (doc.DocumentElement.NamespaceURI == null)
        {
            Log($"ERROR: DocumentElement.NamespaceURI is unavailable for {csproj}");
            return;
        }
        namespaceManager.AddNamespace("msb", doc.DocumentElement.NamespaceURI);

        XmlNodeList hintPaths = doc.SelectNodes("//msb:Reference/msb:HintPath", namespaceManager);
        if (hintPaths != null)
        {
            foreach (XmlNode hintPath in hintPaths)
            {
                if (hintPath.InnerText.Contains(@"packages\"))
                {
                    string cleaned = hintPath.InnerText;
                    while (cleaned.StartsWith(@"..\", StringComparison.Ordinal) || cleaned.StartsWith("../", StringComparison.Ordinal))
                    {
                        cleaned = cleaned.Substring(3);
                    }

                    string newHintPath;
                    if (stepsUp == 0)
                    {
                        newHintPath = cleaned;
                    }
                    else
                    {
                        string prefix = string.Concat(Enumerable.Repeat(@".." + Path.DirectorySeparatorChar, stepsUp));
                        newHintPath = prefix + cleaned;
                    }

                    if (hintPath.InnerText != newHintPath)
                    {
                        hintPath.InnerText = newHintPath;
                        changed = true;
                    }
                }
            }
        }

        if (changed)
        {
            doc.Save(csproj);
            Log($"Updated project package hint paths for {csproj}");
        }
    }

    private void Log(string message)
    {
        string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}";

        try
        {
            if (_isDebug)
            {
                lock (_logLock)
                {
                    string logFile = Path.Combine(_extensionDirectory, "template_logs.txt");
                    File.AppendAllText(logFile, logEntry);
                }
            }
        }
        catch
        {
            System.Diagnostics.Debug.WriteLine($"Failed to write to log file: {logEntry}");
        }
    }

    private static string EnsureExtensionDirectoryExists()
    {
        string localAppDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string thisAppName = "WindowsProjectTemplates";
        string appDirectory = Path.Combine(localAppDir, thisAppName);

        Directory.CreateDirectory(appDirectory);
        return appDirectory;
    }
}
