using DotNetFramework.Business.Modules.Sample.DTOs;
using DotNetFramework.Core.Logging;
using DotNetFramework.Data;
using DotNetFramework.Data.Entities;
using System.Collections.Generic;

namespace DotNetFramework.Business.Modules.Sample.DomainServices
{
    public class FlatUIColorProvider
    {
        private readonly ILogger _logger;

        public FlatUIColorProvider(ILogger logger)
        {
            _logger = logger;
        }

        public IEnumerable<FlatColorDto> GetFlatColors()
        {
            List<FlatColorDto> flatColors = [];

            foreach (FlatColor colorEntity in EmbeddedDataAccess.ReadFlatColors(_logger))
            {
                flatColors.Add(FlatColorDto.MapToDto(colorEntity));
            }

            return flatColors;
        }
    }
}
