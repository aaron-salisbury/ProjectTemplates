using CommunityToolkit.Mvvm.Input;
using Win10App.Base.Helpers;
using Win10Core.SampleTools;

namespace Win10App.ViewModels
{
    public class UUIDGeneratorViewModel : BaseViewModel
    {
        public UUIDGenerator UUIDGenerator { get; set; }

        public RelayCommand ExecuteTaskCommand { get; }

        public RelayCommand CopyUUIDCommand { get; }

        public UUIDGeneratorViewModel()
        {
            UUIDGenerator = new UUIDGenerator(AppLogger);
            CopyUUIDCommand = new RelayCommand(() => PlatformShim.CopyToClipboard(UUIDGenerator.UUID));

            bool uuidGenerateFunction() => UUIDGenerator.Initiate();
            ExecuteTaskCommand = new RelayCommand(async () => await InitiateProcessAsync(uuidGenerateFunction, ExecuteTaskCommand), () => !IsBusy);
        }
    }
}
