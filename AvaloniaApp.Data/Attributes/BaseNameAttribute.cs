using System;

namespace AvaloniaApp.Data.Attributes
{
    /// <summary>
    /// Specify what the filename (without extension) should be, in the event this application element gets written to a file.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
    public class BaseNameAttribute : Attribute
    {
        public string BaseName { get; private set; }

        public BaseNameAttribute(string baseName)
        {
            BaseName = baseName;
        }
    }
}
