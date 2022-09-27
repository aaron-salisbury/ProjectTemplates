using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using Win98Core.SampleTools;

namespace Win98Core.SampleDataAccess
{
    public static class CRUD
    {
        private const string FLAT_COLOR_ELEMENT_NAME = "FlatColor";

        public static List<FlatColor> ReadAllFlatColors()
        {
            List<FlatColor> flatColors = new List<FlatColor>();

            XmlTextReader reader = new XmlTextReader(new StringReader(GetEmbeddedResourceText("Win98Core.SampleDataAccess.FlatColors.xml")));
            reader.ReadToFollowing(FLAT_COLOR_ELEMENT_NAME);

            do
            {
                string flatColorHex = reader.GetAttribute("hex");
                string flatColorName = reader.ReadElementContentAsString();
                flatColors.Add(new FlatColor(flatColorName, flatColorHex));
            } while (reader.ReadToFollowing(FLAT_COLOR_ELEMENT_NAME));

            return flatColors;
        }

        private static string GetEmbeddedResourceText(string filename)
        {
            string result = string.Empty;

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename))
            using (StreamReader streamReader = new StreamReader(stream))
            {
                result = streamReader.ReadToEnd();
            }

            return result;
        }
    }
}
