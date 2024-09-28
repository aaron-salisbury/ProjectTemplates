using System;

namespace DotNet.Business.Modules.Sample.MessageContracts
{
    public record TextSorted
    {
        public required string? SortedText { get; init; }
    }
}
