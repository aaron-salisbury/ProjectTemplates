using GalaSoft.MvvmLight.Command;
using System.Windows;
using Win7Core.SampleTools;

namespace Win7App.ViewModels.SampleTools
{
    public class UUIDGeneratorViewModel : BaseViewModel
    {
        public UUIDGenerator UUIDGenerator { get; set; }

        private readonly RelayCommand _executeTaskCommand;
        public RelayCommand ExecuteTaskCommand
        {
            get { return _executeTaskCommand; }
        }

        private readonly RelayCommand _copyUUIDCommand;
        public RelayCommand CopyUUIDCommand
        {
            get { return _copyUUIDCommand; }
        }

        public UUIDGeneratorViewModel()
        {
            UUIDGenerator = new UUIDGenerator(AppLogger);
            _copyUUIDCommand = new RelayCommand(() => Clipboard.SetText(UUIDGenerator.UUID ?? string.Empty));
            _executeTaskCommand = new RelayCommand(async () => await InitiateProcessAsync(UUIDGenerator.Initiate, ExecuteTaskCommand), () => !IsBusy);
        }
    }
}
