using AvaloniaApp.Business.Modules.Sample.DTOs;
using AvaloniaApp.Data.Entities.Sample;
using Riok.Mapperly.Abstractions;

namespace AvaloniaApp.Business.Modules.Sample
{
    [Mapper]
    internal partial class SampleMapper
    {
        internal partial TTarget MapToDto<TTarget>(object source);

        private partial FlatColorDto MapToDto(FlatColor source);
    }
}
