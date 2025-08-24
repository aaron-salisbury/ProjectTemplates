using Build.DTOs;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.MSBuild;
using Cake.Core.Diagnostics;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("Compile Source Template Projects")]
[IsDependentOn(typeof(LintingTask))]
[IsDependentOn(typeof(ProcessImagesTask))]
[TaskDescription("Compiles all template projects.")]
public sealed class CompileProjectsTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        foreach (TemplateProject templateProject in context.TemplateProjects)
        {
            if (templateProject.IsSdkStyleProject)
            {
                context.DotNetBuild(templateProject.CsprojFilePathAbsolute, new DotNetBuildSettings
                {
                    Configuration = context.Config.ToString(),
                    NoRestore = true
                });
            }
            else
            {
                context.MSBuild(templateProject.CsprojFilePathAbsolute, new MSBuildSettings
                {
                    Target = "Build",
                    Configuration = context.Config.ToString(),
                    Verbosity = Verbosity.Minimal
                });
            }
        }
    }
}
