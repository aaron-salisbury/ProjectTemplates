using DotNetFramework.Business.Modules.Sample.ApplicationServices;
using DotNetFramework.Business.Modules.Sample.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Win7App.Base;
using Win7App.Base.MvvmInput;

namespace Win7App.ViewModels.SampleTools;

public class FlatUIColorPickerViewModel : BaseViewModel
{
    public RelayCommand<string> ColorClickCommand { get; }
    public RelayCommand CopyHexCommand { get; }

    private string _selectedName;
    public string SelectedName
    {
        get { return _selectedName; }
        set { SetField(ref _selectedName, value, nameof(SelectedName)); }
    }

    private string _selectedHex;
    public string SelectedHex
    {
        get { return _selectedHex; }
        set { SetField(ref _selectedHex, value, nameof(SelectedHex)); }
    }

    private List<FlatColorDto> _flatColors;
    public List<FlatColorDto> FlatColors
    {
        get { return _flatColors; }
        set { SetField(ref _flatColors, value, nameof(FlatColors)); }
    }

    public FlatUIColorPickerViewModel(ISampleToolsService sampleToolsService)
    {
        FlatColors = sampleToolsService.GetFlatColors().ToList();

        ColorClickCommand = new RelayCommand<string>((s) => SelectColor(s));
        CopyHexCommand = new RelayCommand(() => Clipboard.SetText(SelectedHex ?? string.Empty));
    }

    /// <param name="commandParameter">Hex value of the FlatColor selected from the FlatUIColorPicker.FlatColors collection.</param>
    private void SelectColor(object commandParameter)
    {
        FlatColorDto flatColor = FlatColors.Where(fc => fc.Hex.Equals(commandParameter.ToString())).FirstOrDefault();

        string flatColorName = null;
        string flatColorHex = null;
        if (flatColor != null)
        {
            flatColorName = flatColor.Name;
            flatColorHex = flatColor.Hex;
        }

        SelectedName = flatColorName;
        SelectedHex = flatColorHex;
    }
}
