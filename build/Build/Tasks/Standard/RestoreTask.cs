using Build.DTOs;
using Cake.Common;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.MSBuild;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using System;

namespace Build.Tasks.Standard;

[TaskName("Restore Source Template Projects")]
[IsDependentOn(typeof(CleanTask))]
[TaskDescription("Restores the NuGet packages for the template projects and checks for known vulnerabilities in dependencies.")]
public sealed class RestoreTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.Log.Information("Restoring NuGet packages for the template projects...");

        foreach (ReleaseProject project in context.TemplateProjects)
        {
            RestoreProject(context, project);
        }
    }

    private static void RestoreProject(BuildContext context, ReleaseProject project)
    {
        if (project.IsSdkStyleProject)
        {
            context.DotNetRestore(project.CsprojFilePathAbsolute);
            context.Log.Information($"{Environment.NewLine}Checking {project.Name} for vulnerabilities...");
            context.StartProcess("dotnet", $"list \"{project.CsprojFilePathAbsolute}\" package --vulnerable");
        }
        else
        {
            // For legacy projects, use MS Build restore.
            context.MSBuild(project.CsprojFilePathAbsolute, new MSBuildSettings
            {
                Target = "Restore"
            });
        }
    }
}
