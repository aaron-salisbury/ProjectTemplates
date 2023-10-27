using AvaloniaApp.Data.Domains;
using System.Diagnostics;
using System.Reflection;
using System.Xml.Linq;

namespace AvaloniaApp.Data
{
    public static class Access
    {
        private const string EMBEDDED_COLORS_ABSOLUTE_FILEPATH = "AvaloniaApp.Data.Resources.FlatColors.xml";

        public static IEnumerable<FlatColor> ReadFlatColors()
        {
            List<FlatColor> flatColors = new();

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
                Debug.WriteLine($"Failed to retrieve flat colors: {ex.Message}", "ERROR");
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
}
