using DotNetFramework.Data.Entities;
using DotNetFrameworkToolkit.Core;
using DotNetFrameworkToolkit.Modules.DataAccess.FileSystem;
using DotNetFrameworkToolkit.Modules.Logging;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace DotNetFramework.Data
{
    /// <summary>
    /// Read Embedded data.
    /// </summary>
    public sealed class EmbeddedDataAccess : IEmbeddedDataAccess
    {
        private const string EMBEDDED_COLORS_ABSOLUTE_FILEPATH = "Win98.Data.Base.Resources.FlatColors.xml";

        private readonly IFileSystemAccess _fileSystemAccess;
        private readonly ILogger _logger;

        public EmbeddedDataAccess(IFileSystemAccess fileSystemAccess, ILogger logger)
        {
            _fileSystemAccess = fileSystemAccess;
            _logger = logger;
        }

        public List<FlatColor> ReadFlatColors()
        {
            const string FLAT_COLOR_ELEMENT_NAME = "FlatColor";

            List<FlatColor> flatColors = [];

            ProcessResult<string> embeddedReadResult = _fileSystemAccess.GetEmbeddedResourceText(Assembly.GetExecutingAssembly(), EMBEDDED_COLORS_ABSOLUTE_FILEPATH);
            if (!embeddedReadResult.IsSuccessful)
            {
                _logger.LogError(embeddedReadResult.Error, "EmbeddedDataAccess.ReadFlatColors: Failed to read embedded resource text for '{0}'", EMBEDDED_COLORS_ABSOLUTE_FILEPATH);
                return flatColors;
            }

            XmlTextReader reader = new(new StringReader(embeddedReadResult.Value));
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
