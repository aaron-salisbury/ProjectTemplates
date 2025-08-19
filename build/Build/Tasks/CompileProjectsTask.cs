using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.MSBuild;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using System.IO;

namespace Build.Tasks;

[TaskName("Compile Projects")]
[IsDependentOn(typeof(LintingTask))]
[IsDependentOn(typeof(ProcessImagesTask))]
[TaskDescription("Compiles all projects in the src directory.")]
public sealed class CompileProjectsTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        string sourceDir = Path.Combine(context.AbsolutePathToRepo, "src");
        string[] allProjectFiles = Directory.GetFiles(sourceDir, "*.csproj", SearchOption.AllDirectories);

        foreach (string csprojPath in allProjectFiles)
        {
            if (string.Equals(Path.GetFileNameWithoutExtension(csprojPath), "ProjectTemplates"))
            {
                // Exclude ProjectTemplates VS extension project as it will be compiled after templates & packages have been added to it.
                continue;
            }

            if (BuildContext.IsSdkStyleProject(csprojPath))
            {
                context.DotNetBuild(csprojPath, new DotNetBuildSettings
                {
                    Configuration = context.Config.ToString(),
                    NoRestore = true
                });
            }
            else
            {
                context.MSBuild(csprojPath, new MSBuildSettings
                {
                    Target = "Build",
                    Configuration = context.Config.ToString(),
                    Verbosity = Verbosity.Minimal
                });
            }
        }
    }
}
