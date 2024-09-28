using System;

namespace DotNet.Business.Modules.Sample.MessageContracts
{
    public record GuidGenerated
    {
        public required string UUID { get; init; }
    }
}
