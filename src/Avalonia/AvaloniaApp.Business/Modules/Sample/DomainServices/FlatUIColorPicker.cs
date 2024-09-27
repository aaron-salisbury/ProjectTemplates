using AvaloniaApp.Business.Modules.Sample.DTOs;
using AvaloniaApp.Data;
using Microsoft.Extensions.Logging;

namespace AvaloniaApp.Business.Modules.Sample.DomainServices
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
            SampleMapper mapper = new();

            return EmbeddedDataAccess.ReadFlatColors(_logger)
                .Select(entity => mapper.MapToDto<FlatColorDto>(entity));
        }
    }
}
