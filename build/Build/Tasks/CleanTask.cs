using Cake.Common.IO;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("Clean")]
[TaskDescription("Deletes the Debug or Release directories in the project bin directories.")]
public sealed class CleanTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        string srcDir = System.IO.Path.Combine(context.AbsolutePathToRepo, "src");
        DirectoryPathCollection compileDirs = context.GetDirectories($"{srcDir}/**/bin/{context.Config}");

        foreach (DirectoryPath dir in compileDirs)
        {
            context.CleanDirectory(dir);
            context.Log.Information($"Cleaned {dir}");
        }
    }
}
