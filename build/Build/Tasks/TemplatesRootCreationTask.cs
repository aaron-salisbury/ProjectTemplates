using Cake.Core.Diagnostics;
using Cake.Frosting;
using System;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace Build.Tasks;

[TaskName("Build Root Templates")]
[IsDependentOn(typeof(TemplatesModificationTask))]
[TaskDescription("Combines individual project templates into root templates.")]
public sealed class TemplatesRootCreationTask : FrostingTask<BuildContext>
{
    public override bool ShouldRun(BuildContext context)
    {
        return true;
        //return context.Config == BuildConfigurations.Release;
    }

    public override void Run(BuildContext context)
    {
        // Ultimately, we want the user to be able to select one template to generate the entire solution project structure.

        Stopwatch stopwatch = Stopwatch.StartNew();
        context.Log.Information($"Building root templates...");

        //TODO:

        stopwatch.Stop();
        double completionTime = Math.Round(stopwatch.Elapsed.TotalSeconds, 1);
        context.Log.Information($"Creation of root templates complete ({completionTime}s)");
    }

    private static bool IsProjectAnApplication(string csprojPath, BuildContext context)
    {
        try
        {
            XDocument doc = XDocument.Load(csprojPath);
            XElement? outputTypeElement = doc.Descendants("OutputType").FirstOrDefault();

            if (outputTypeElement != null)
            {
                string value = outputTypeElement.Value.Trim();
                return value.Equals("Exe", StringComparison.OrdinalIgnoreCase) || value.Equals("WinExe", StringComparison.OrdinalIgnoreCase);
            }

            // If OutputType is missing, assume library (per SDK-style convention).
            return false;
        }
        catch (Exception ex)
        {
            context.Log.Error($"Could not determine OutputType for {csprojPath}: {ex.Message}");
            return false;
        }
    }
}
