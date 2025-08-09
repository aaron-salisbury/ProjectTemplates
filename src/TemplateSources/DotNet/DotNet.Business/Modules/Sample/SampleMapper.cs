using DotNet.Business.Modules.Sample.DTOs;
using DotNet.Data.Entities.Sample;
using Riok.Mapperly.Abstractions;

namespace DotNet.Business.Modules.Sample
{
    [Mapper]
    internal partial class SampleMapper
    {
        internal partial TTarget MapToDto<TTarget>(object source);

        private partial FlatColorDto MapToDto(FlatColor source);
    }
}
