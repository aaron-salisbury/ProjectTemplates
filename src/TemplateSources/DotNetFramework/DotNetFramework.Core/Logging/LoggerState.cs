using System.Collections.Generic;

namespace DotNetFramework.Core.Logging
{
    public class LoggerState
    {
        public string SinglePropertyName { get; set; }
        public object SinglePropertyValue { get; set; }
        public Dictionary<string, object> PropertyValuesByNames { get; set; }

        public bool IsSingleProperty => SinglePropertyName != null && SinglePropertyValue != null;
    }
}
