using Build.DTOs;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.MSBuild;
using Cake.Core.Diagnostics;
using Cake.Frosting;

namespace Build.Tasks.Standard;

[TaskName("Compile Source Template Projects")]
[IsDependentOn(typeof(LintingTask))]
[IsDependentOn(typeof(ProcessImagesTask))]
[TaskDescription("Compiles all template projects.")]
public sealed class CompileProjectsTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        foreach (ReleaseProject project in context.TemplateProjects)
        {
            CompileProject(context, project);
        }
    }

    private static void CompileProject(BuildContext context, ReleaseProject project)
    {
        if (project.IsSdkStyleProject)
        {
            context.DotNetBuild(project.CsprojFilePathAbsolute, new DotNetBuildSettings
            {
                Configuration = context.Config.ToString(),
                NoRestore = true
            });
        }
        else
        {
            context.MSBuild(project.CsprojFilePathAbsolute, new MSBuildSettings
            {
                Target = "Build",
                Configuration = context.Config.ToString(),
                Verbosity = Verbosity.Minimal
            });
        }
    }
}
