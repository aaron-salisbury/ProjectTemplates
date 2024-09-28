using DotNet.Data.Entities.Sample;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Xml.Linq;

namespace DotNet.Data;

/// <summary>
/// Read Embedded data data.
/// </summary>
public static class EmbeddedDataAccess
{
    private const string EMBEDDED_COLORS_ABSOLUTE_FILEPATH = "DotNet.Data.Base.Resources.FlatColors.xml";

    public static IEnumerable<FlatColor> ReadFlatColors(ILogger logger)
    {
        List<FlatColor> flatColors = [];

        try
        {
            string? xml = GetEmbeddedResourceText(EMBEDDED_COLORS_ABSOLUTE_FILEPATH);

            if (!string.IsNullOrEmpty(xml))
            {
                XDocument xDoc = new();
                xDoc = XDocument.Parse(xml);

                foreach (XElement colorElement in xDoc.Descendants())
                {
                    string? hex = colorElement.Attribute("hex")?.Value;

                    if (!string.IsNullOrEmpty(hex))
                    {
                        flatColors.Add(new FlatColor()
                        {
                            Name = colorElement.Value,
                            Hex = hex
                        });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to retrieve flat colors.");
        }

        return flatColors;
    }

    private static string? GetEmbeddedResourceText(string absoluteFilepath)
    {
        string? result = null;

        using (Stream? manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(absoluteFilepath))
        {
            if (manifestResourceStream != null)
            {
                using StreamReader streamReader = new(manifestResourceStream);
                result = streamReader.ReadToEnd();
            }
        }

        return result;
    }
}
