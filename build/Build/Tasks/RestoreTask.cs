using Cake.Common;
using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.MSBuild;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Frosting;
using System;

namespace Build.Tasks;

[TaskName("Restore")]
[IsDependentOn(typeof(CleanTask))]
[TaskDescription("Restores the NuGet packages for the solution and checks for known vulnerabilities in dependencies.")]
public sealed class RestoreTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.Log.Information("Restoring NuGet packages for the solution...");

        string srcDir = System.IO.Path.Combine(context.AbsolutePathToRepo, "src");
        FilePathCollection csprojFiles = context.GetFiles(System.IO.Path.Combine(srcDir, "**/*.csproj"));

        foreach (FilePath csproj in csprojFiles)
        {
            if (BuildContext.IsSdkStyleProject(csproj.FullPath))
            {
                context.DotNetRestore(csproj.FullPath);
                context.Log.Information($"{Environment.NewLine}Checking {csproj.GetFilenameWithoutExtension()} for vulnerabilities...");
                context.StartProcess("dotnet", $"list \"{csproj.FullPath}\" package --vulnerable");
            }
            else
            {
                // For legacy projects, use MS Build restore.
                context.MSBuild(csproj.FullPath, new MSBuildSettings
                {
                    Target = "Restore"
                });
            }
        }
    }
}
