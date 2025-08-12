using DotNet.Data.Entities;
using Microsoft.Extensions.Logging;
using RunnethOverStudio.AppToolkit.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

namespace DotNet.Data;

/// <summary>
/// Read Embedded data.
/// </summary>
public static class EmbeddedDataAccess
{
    private const string EMBEDDED_COLORS_FILEPATH = "DotNet.Data.Resources.FlatColors.xml";

    public static IEnumerable<FlatColor> ReadFlatColors(ILogger logger)
    {
        List<FlatColor> flatColors = [];

        try
        {
            string? xml = Assembly.GetExecutingAssembly().ReadResource(EMBEDDED_COLORS_FILEPATH);

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
}
