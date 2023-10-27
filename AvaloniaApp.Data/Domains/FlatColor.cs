using System;

namespace AvaloniaApp.Data.Domains
{
    [Serializable]
    public class FlatColor
    {
        public string Name { get; set; } = null!;
        public string Hex { get; set; } = null!;
    }
}
