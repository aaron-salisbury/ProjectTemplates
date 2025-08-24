using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("Default")]
[IsDependentOn(typeof(UpdateAndBuildVSIX))]
[TaskDescription("Entry point for the build process when a command-line target isn't specified.")]
public sealed class DefaultTask : FrostingTask
{
    public override void Run(ICakeContext context)
    {
        context.Log.Information("Set specific task to run with --target [task]");
        context.Log.Information("Run entire release build with: dotnet run -- --configuration=Release");
    }
}
