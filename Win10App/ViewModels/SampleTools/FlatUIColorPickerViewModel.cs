using CommunityToolkit.Mvvm.Input;
using System.Linq;
using Win10App.Base.Helpers;
using Win10Core.SampleTools;

namespace Win10App.ViewModels
{
    public class FlatUIColorPickerViewModel : BaseViewModel
    {
        public FlatUIColorPicker FlatUIColorPicker { get; set; }

        public RelayCommand<string> ColorClickCommand { get; }

        public RelayCommand CopyHexCommand { get; }

        private string _selectedName;
        public string SelectedName
        {
            get => _selectedName;
            set => SetProperty(ref _selectedName, value);
        }

        private string _selectedHex;
        public string SelectedHex
        {
            get => _selectedHex;
            set => SetProperty(ref _selectedHex, value);
        }

        public FlatUIColorPickerViewModel()
        {
            FlatUIColorPicker = new FlatUIColorPicker(AppLogger);
            FlatUIColorPicker.SetFlatColors();
            ColorClickCommand = new RelayCommand<string>((s) => SelectColor(s));
            CopyHexCommand = new RelayCommand(() => PlatformShim.CopyToClipboard(SelectedHex));
        }

        /// <param name="commandParameter">Hex value of the FlatColor selected from the FlatUIColorPicker.FlatColors collection.</param>
        private void SelectColor(object commandParameter)
        {
            FlatColor flatColor = FlatUIColorPicker.FlatColors.Where(fc => fc.Hex.Equals(commandParameter.ToString())).FirstOrDefault();
            SelectedName = flatColor?.Name;
            SelectedHex = flatColor?.Hex;
        }
    }
}
