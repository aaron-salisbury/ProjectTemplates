using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using Win7Core.SampleTools;
using System.Threading.Tasks;
using System.Collections.Generic;
using Win7App.Base;
using System;
using System.Linq;

namespace Win7App.ViewModels.SampleTools
{
    public class LineSorterViewModel : BaseViewModel
    {
        public LineSorter LineSorter { get; set; }

        public RelayCommand ExecuteTaskCommand { get; }

        private List<ComboBoxEnumItem> _sortTypes;
        public List<ComboBoxEnumItem> SortTypes
        {
            get => _sortTypes;
            set
            {
                _sortTypes = value;
                RaisePropertyChanged(nameof(SortTypes));
            }
        }

        private ComboBoxEnumItem _selectedSortType;
        public ComboBoxEnumItem SelectedSortType
        {
            get => _selectedSortType;
            set
            {
                _selectedSortType = value;
                RaisePropertyChanged(nameof(SelectedSortType));
                LineSorter.SelectedSortType = (LineSorter.SortTypes)value.Value;
            }
        }

        public LineSorterViewModel()
        {
            LineSorter = new LineSorter(AppLogger);
            ExecuteTaskCommand = new RelayCommand(async () => await InitiateProcessAsync(), () => !IsBusy);

            SortTypes = Enum.GetValues(typeof(LineSorter.SortTypes))
                .Cast<LineSorter.SortTypes>()
                .Select(st => new ComboBoxEnumItem() { Value = (int)st, Text = LineSorter.GetSortTypeDisplayName(st) })
                .ToList();

            SelectedSortType = SortTypes
                .Where(cbi => cbi.Value == (int)LineSorter.SelectedSortType)
                .First();
        }

        private async Task InitiateProcessAsync()
        {
            try
            {
                IsBusy = true;
                await ExportDataAsync().ConfigureAwait(false);
            }
            finally
            {
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    IsBusy = false;
                    ExecuteTaskCommand.RaiseCanExecuteChanged();
                });
            }
        }

        private Task<bool> ExportDataAsync()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            Task.Run(() =>
            {
                // Do long running synchronous work here...
                bool processIsSuccessful = LineSorter.Initiate();

                tcs.SetResult(processIsSuccessful);
            }).ConfigureAwait(false);

            return tcs.Task;
        }
    }
}
