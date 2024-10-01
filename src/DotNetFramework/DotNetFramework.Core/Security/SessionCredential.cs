using System;

namespace DotNetFramework.Core.Security
{
    public class SessionCredential
    {
        public byte[] Salt { get; set; }
        public byte[] Hash { get; set; }
        public int WorkFactor { get; set; }
    }
}
