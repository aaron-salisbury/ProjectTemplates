using System;

namespace DotNet.Business.Modules.Sample.Events
{
    public class GuidGenerated : EventArgs
    {
        public required string UUID { get; init; }
    }
}
