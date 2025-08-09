using DotNetFramework.Core;
using DotNetFramework.Core.Logging;
using DotNetFramework.Data.Entities;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace DotNetFramework.Data
{
    /// <summary>
    /// Read Embedded data.
    /// </summary>
    public static class EmbeddedDataAccess
    {
        private const string EMBEDDED_COLORS_ABSOLUTE_FILEPATH = "Win98.Data.Base.Resources.FlatColors.xml";

        public static List<FlatColor> ReadFlatColors(ILogger logger)
        {
            const string FLAT_COLOR_ELEMENT_NAME = "FlatColor";

            List<FlatColor> flatColors = new List<FlatColor>();

            XmlTextReader reader = new XmlTextReader(new StringReader(IO.GetEmbeddedResourceText(Assembly.GetExecutingAssembly(), EMBEDDED_COLORS_ABSOLUTE_FILEPATH, logger)));
            reader.ReadToFollowing(FLAT_COLOR_ELEMENT_NAME);

            do
            {
                string flatColorHex = reader.GetAttribute("hex");
                string flatColorName = reader.ReadElementContentAsString();
                flatColors.Add(new FlatColor() { Name = flatColorName, Hex = flatColorHex });
            } while (reader.ReadToFollowing(FLAT_COLOR_ELEMENT_NAME));

            return flatColors;
        }
    }
}
