using GalaSoft.MvvmLight.Command;
using System.Linq;
using System.Windows;
using Win7Core.SampleTools;

namespace Win7App.ViewModels.SampleTools
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
            set => Set(ref _selectedName, value);
        }

        private string _selectedHex;
        public string SelectedHex
        {
            get => _selectedHex;
            set => Set(ref _selectedHex, value);
        }

        public FlatUIColorPickerViewModel()
        {
            FlatUIColorPicker = new FlatUIColorPicker(AppLogger);
            FlatUIColorPicker.SetFlatColors();
            ColorClickCommand = new RelayCommand<string>((s) => SelectColor(s));
            CopyHexCommand = new RelayCommand(() => Clipboard.SetText(SelectedHex ?? string.Empty));
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
