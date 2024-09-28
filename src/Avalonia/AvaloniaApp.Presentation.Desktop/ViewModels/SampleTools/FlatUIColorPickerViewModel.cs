using AvaloniaApp.Business.Modules.Sample.ApplicationServices;
using AvaloniaApp.Business.Modules.Sample.DTOs;
using AvaloniaApp.Presentation.Desktop.Base;
using System.Collections.Generic;
using System.Linq;

namespace AvaloniaApp.Presentation.Desktop.ViewModels;

public partial class FlatUIColorPickerViewModel : BaseViewModel
{
    public List<FlatColorDto> FlatColors { get; set; }

    public FlatUIColorPickerViewModel(ISampleToolsService sampleToolsService)
    {
        FlatColors = sampleToolsService.GetFlatColors().ToList();
    }
}
