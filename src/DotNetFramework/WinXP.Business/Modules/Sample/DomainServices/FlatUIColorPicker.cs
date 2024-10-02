using DotNetFramework.Core.Logging;
using DotNetFramework.Data;
using System.Collections.Generic;
using System.Linq;
using WinXP.Business.Modules.Sample.DTOs;

namespace WinXP.Business.Modules.Sample.DomainServices
{
    public class FlatUIColorPicker
    {
        private readonly ILogger _logger;

        public FlatUIColorPicker(ILogger logger)
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
