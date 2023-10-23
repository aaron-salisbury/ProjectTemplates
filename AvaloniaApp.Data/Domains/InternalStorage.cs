using System;

namespace AvaloniaApp.Data.Domains
{
    [Serializable]
    internal class InternalStorage
    {
        public string UserStorageDirectory { get; set; } = null!;
    }
}
