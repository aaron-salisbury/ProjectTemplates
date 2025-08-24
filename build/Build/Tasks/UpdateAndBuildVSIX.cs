using Build.DTOs;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("Update and Build VSIX")]
[IsDependentOn(typeof(TemplatesRootCreationTask))]
[TaskDescription("Update VSIX manifest with solution templates and template NuGet packages.")]
public sealed class UpdateAndBuildVSIX : FrostingTask<BuildContext>
{
    public override bool ShouldRun(BuildContext context)
    {
        return true;
        //return context.Config == BuildConfigurations.Release;
    }

    public override void Run(BuildContext context)
    {
        //TODO: Delete all zip files in the VSIX template folder.

        foreach (TemplateProject templateProject in context.TemplateProjects)
        {
            if (!templateProject.IsApplication)
            {
                continue;
            }

            //TODO: Copy template zip into the VSIX template folder, keeping track of the file names.
        }



        //TODO: Distinctly copy each nupkg file that were in the WizardData into the VSIX Packages folder. Make sure each file is set to Copy always in the properties.

        //TODO: Update the VSIX manifest with the new template zip file names and nupkg files.

        //TODO: Compile the VSIX project.

        //TODO: Log the output location of the VSIX file.
    }
}
