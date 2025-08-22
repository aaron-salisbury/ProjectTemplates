using DotNet.Business.Modules.Sample.ApplicationServices;
using DotNet.Business.Modules.Sample.DTOs;
using RunnethOverStudio.AppToolkit.Presentation.MVVM;
using System.Collections.Generic;

namespace AvaloniaApp.ViewModels;

public partial class FlatUIColorPickerViewModel : BaseViewModel
{
    public List<FlatColorDto> FlatColors { get; set; }

    public FlatUIColorPickerViewModel(ISampleToolsService sampleToolsService)
    {
        FlatColors = [.. sampleToolsService.GetFlatColors()];
    }
}
