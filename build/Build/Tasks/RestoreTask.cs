using Build.DTOs;
using Cake.Common;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.MSBuild;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using System;

namespace Build.Tasks;

[TaskName("Restore Source Template Projects")]
[IsDependentOn(typeof(CleanTask))]
[TaskDescription("Restores the NuGet packages for the template projects and checks for known vulnerabilities in dependencies.")]
public sealed class RestoreTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.Log.Information("Restoring NuGet packages for the template projects...");

        foreach (TemplateProject templateProject in context.TemplateProjects)
        {
            if (templateProject.IsSdkStyleProject)
            {
                context.DotNetRestore(templateProject.CsprojFilePathAbsolute);
                context.Log.Information($"{Environment.NewLine}Checking {templateProject.Name} for vulnerabilities...");
                context.StartProcess("dotnet", $"list \"{templateProject.CsprojFilePathAbsolute}\" package --vulnerable");
            }
            else
            {
                // For legacy projects, use MS Build restore.
                context.MSBuild(templateProject.CsprojFilePathAbsolute, new MSBuildSettings
                {
                    Target = "Restore"
                });
            }
        }
    }
}
