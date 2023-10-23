using AvaloniaApp.Data.Attributes;

namespace AvaloniaApp.Business.DTOs
{
    [Serializable, BaseName("AvaloniaApp")]
    internal class AppData
    {
        public string? Version { get; set; }
    }
}
