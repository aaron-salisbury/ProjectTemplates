using GalaSoft.MvvmLight.Command;
using System.Linq;
using System.Windows;
using Win7Core.SampleTools;

namespace Win7App.ViewModels.SampleTools
{
    public class FlatUIColorPickerViewModel : BaseViewModel
    {
        public FlatUIColorPicker FlatUIColorPicker { get; set; }

        private readonly RelayCommand<string> _colorClickCommand;
        public RelayCommand<string> ColorClickCommand
        {
            get { return _colorClickCommand; }
        }

        private readonly RelayCommand _copyHexCommand;
        public RelayCommand CopyHexCommand
        {
            get { return _copyHexCommand; }
        }

        private string _selectedName;
        public string SelectedName
        {
            get { return _selectedName; }
            set { Set(ref _selectedName, value); }
        }

        private string _selectedHex;
        public string SelectedHex
        {
            get { return _selectedHex; }
            set { Set(ref _selectedHex, value); }
        }

        public FlatUIColorPickerViewModel()
        {
            FlatUIColorPicker = new FlatUIColorPicker(AppLogger);
            FlatUIColorPicker.SetFlatColors();
            _colorClickCommand = new RelayCommand<string>((s) => SelectColor(s));
            _copyHexCommand = new RelayCommand(() => Clipboard.SetText(SelectedHex ?? string.Empty));
        }

        /// <param name="commandParameter">Hex value of the FlatColor selected from the FlatUIColorPicker.FlatColors collection.</param>
        private void SelectColor(object commandParameter)
        {
            FlatColor flatColor = FlatUIColorPicker.FlatColors.Where(fc => fc.Hex.Equals(commandParameter.ToString())).FirstOrDefault();

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
}
