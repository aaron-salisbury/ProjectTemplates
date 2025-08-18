using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.MSBuild;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Frosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build.Tasks;

[TaskName("Compile Projects")]
[IsDependentOn(typeof(LintingTask))]
[IsDependentOn(typeof(ProcessImagesTask))]
[TaskDescription("Compiles all projects in the src directory.")]
public sealed class CompileProjectsTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        string srcDir = System.IO.Path.Combine(context.AbsolutePathToRepo, "src");
        FilePathCollection csprojFiles = context.GetFiles(System.IO.Path.Combine(srcDir, "**/*.csproj"));

        // Exclude any .csproj under the ProjectTemplates folder.
        // The project itself will be cpompiled after templates & packages have been added to it,
        // plus it will have many csproj files in it from the template and those should't be compiled.
        List<FilePath> filteredCsprojFiles = [.. csprojFiles.Where(csproj =>
        !csproj.FullPath.Replace('\\', '/').Contains("/ProjectTemplates/", StringComparison.OrdinalIgnoreCase))];

        foreach (FilePath csproj in filteredCsprojFiles)
        {
            if (BuildContext.IsSdkStyleProject(csproj.FullPath))
            {
                context.DotNetBuild(csproj.FullPath, new DotNetBuildSettings
                {
                    Configuration = context.Config.ToString(),
                    NoRestore = true
                });
            }
            else
            {
                context.MSBuild(csproj.FullPath, new MSBuildSettings
                {
                    Target = "Build",
                    Configuration = context.Config.ToString(),
                    Verbosity = Verbosity.Minimal
                });
            }
        }
    }
}
