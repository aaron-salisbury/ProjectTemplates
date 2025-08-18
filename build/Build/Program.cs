using Cake.Frosting;

namespace Build;

/// <summary>
/// Build orchestrator for the ProjectTemplates solution.
/// Uses <see href="https://cakebuild.net/">Cake</see> (C# Make) 
/// which is open source, cross platform, cross environment, cross service, and cross runtime.
/// </summary>
public static class Program
{
    public static int Main(string[] args)
    {
        return new CakeHost()
            .UseContext<BuildContext>()
            .Run(args);
    }
}
