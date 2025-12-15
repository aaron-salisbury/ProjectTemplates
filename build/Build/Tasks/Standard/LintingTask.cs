using Cake.Common;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using System;
using System.Diagnostics;

namespace Build.Tasks.Standard;

[TaskName("Linting")]
[IsDependentOn(typeof(RestoreTask))]
[TaskDescription("Applies style preferences and static analysis recommendations to projects.")]
public sealed class LintingTask : FrostingTask<BuildContext>
{
    // ref: https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-format
    //      https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/
    //      https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/configuration-files#editorconfig

    public override void Run(BuildContext context)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        context.Log.Information($"Formatting code...");

        string[] solutionPaths = System.IO.Directory.GetFiles(context.SourceDirectory, "*.sln", System.IO.SearchOption.AllDirectories);

        foreach (string solutionPath in solutionPaths)
        {
            // Non-SDK-style projects will be skipped with a warning, but the process will continue for the rest.
            // If we ever want to ensure Non-SDK-style projects are formatted, consider using legacy tools.
            context.Log.Information($"Formatting solution: {System.IO.Path.GetFileName(solutionPath)}");
            context.StartProcess("dotnet", $"format \"{solutionPath}\" --no-restore");
        }

        stopwatch.Stop();
        double completionTime = Math.Round(stopwatch.Elapsed.TotalSeconds, 1);
        context.Log.Information($"Linting complete ({completionTime}s)");
    }
}
