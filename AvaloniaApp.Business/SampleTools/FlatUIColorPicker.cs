using AvaloniaApp.Business.Base;
using AvaloniaApp.Data;
using AvaloniaApp.Data.Domains;

namespace AvaloniaApp.Business.SampleTools
{
    public class FlatUIColorPicker : ObservableModel
    {
        public static IEnumerable<FlatColor> FlatColors { get; set; } = Access.ReadFlatColors();
    }
}
