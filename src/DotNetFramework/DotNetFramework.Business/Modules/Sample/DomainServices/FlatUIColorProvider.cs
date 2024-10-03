using DotNetFramework.Business.Modules.Sample.DTOs;
using DotNetFramework.Core.Logging;
using DotNetFramework.Data;
using System.Collections.Generic;
using System.Linq;

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
            return EmbeddedDataAccess.ReadFlatColors(_logger)
                .Select(entity => FlatColorDto.MapToDto(entity));
        }
    }
}
