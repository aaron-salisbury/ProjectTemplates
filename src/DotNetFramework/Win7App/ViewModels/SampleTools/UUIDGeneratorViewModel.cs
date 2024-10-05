using DotNetFramework.Business.Modules.Sample.ApplicationServices;
using System.Windows;
using Win7App.Base;
using Win7App.Base.MvvmInput;

namespace Win7App.ViewModels.SampleTools
{
    public class UUIDGeneratorViewModel : BaseViewModel
    {
        public RelayCommand ExecuteTaskCommand { get; }
        public RelayCommand CopyUUIDCommand { get; }

        bool _shouldCapitalize;
        public bool ShouldCapitalize
        {
            get => _shouldCapitalize;
            set => SetField(ref _shouldCapitalize, value, nameof(ShouldCapitalize));
        }

        string _uUID;
        public string UUID
        {
            get => _uUID;
            set => SetField(ref _uUID, value, nameof(UUID));
        }

        private readonly ISampleToolsService _sampleToolsService;

        public UUIDGeneratorViewModel(ISampleToolsService sampleToolsService)
        {
            _sampleToolsService = sampleToolsService;

            CopyUUIDCommand = new RelayCommand(() => Clipboard.SetText(UUID ?? string.Empty));
            ExecuteTaskCommand = new RelayCommand(() => Generate());
        }

        private bool Generate()
        {
            string generatedUUID = _sampleToolsService.InitializeGUIDGeneration(ShouldCapitalize);

            UUID = generatedUUID;

            return generatedUUID != null;
        }
    }
}
