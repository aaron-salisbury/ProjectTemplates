﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Win10Core.SampleTools;

namespace Win10Core.SampleDataAccess
{
    public static class CRUD
    {
        public static List<FlatColor> ReadAllFlatColors()
        {
            XDocument flatColorsXDoc = new XDocument();
            flatColorsXDoc = XDocument.Parse(GetEmbeddedResourceText("Win10Core.SampleDataAccess.FlatColors.xml"));

            return flatColorsXDoc.Element("FlatColors")
                .Descendants()
                .Select(fc => new FlatColor() { Name = fc.Value, Hex = fc.Attribute("hex").Value })
                .ToList();
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
