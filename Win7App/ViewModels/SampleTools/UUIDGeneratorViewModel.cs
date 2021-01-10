using GalaSoft.MvvmLight.Command;
using System.Windows;
using Win7Core.SampleTools;

namespace Win7App.ViewModels.SampleTools
{
    public class UUIDGeneratorViewModel : BaseViewModel
    {
        public UUIDGenerator UUIDGenerator { get; set; }

        public RelayCommand ExecuteTaskCommand { get; }

        public RelayCommand CopyUUIDCommand { get; }

        public UUIDGeneratorViewModel()
        {
            UUIDGenerator = new UUIDGenerator(AppLogger);
            CopyUUIDCommand = new RelayCommand(() => Clipboard.SetText(UUIDGenerator.UUID ?? string.Empty));

            bool sortLinesfunction() => UUIDGenerator.Initiate();
            ExecuteTaskCommand = new RelayCommand(async () => await InitiateProcessAsync(sortLinesfunction, ExecuteTaskCommand), () => !IsBusy);
        }
    }
}
