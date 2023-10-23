using AvaloniaApp.Business.SampleTools;
using AvaloniaApp.Presentation.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApp.Presentation.ViewModels
{
    public partial class UUIDGeneratorViewModel : BaseViewModel
    {
        public RelayCommand GenerateCommand { get; }

        [ObservableProperty]
        bool _capitalize;

        [ObservableProperty]
        string? _uUID;

        private UUIDGenerator _uUIDGenerator;

        public UUIDGeneratorViewModel()
        {
            _uUIDGenerator = new UUIDGenerator();

            GenerateCommand = new RelayCommand(GenerateUUID);
        }

        private void GenerateUUID()
        {
            _uUIDGenerator.Capitalize = Capitalize;

            if (_uUIDGenerator.Initiate())
            {
                UUID = _uUIDGenerator.UUID;
            }
        }
    }
}
