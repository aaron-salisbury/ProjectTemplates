using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Win10App.Base.Extensions;
using Win10App.Base.Helpers;
using Win10Core.SampleTools;

namespace Win10App.ViewModels
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
            ExecuteTaskCommand = new RelayCommand(async () => await InitiateProcess(), () => !IsBusy);

            SortTypes = Enum.GetValues(typeof(LineSorter.SortTypes))
                .Cast<LineSorter.SortTypes>()
                .Select(st => new ComboBoxEnumItem() { Value = (int)st, Text = st.GetAttribute<DisplayAttribute>()?.Name ?? st.ToString() })
                .ToList();

            SelectedSortType = SortTypes
                .Where(cbi => cbi.Value == (int)LineSorter.SelectedSortType)
                .First();
        }

        private async Task InitiateProcess()
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
                bool processIsSuccessful = false;

                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    processIsSuccessful = LineSorter.Initiate();
                });

                tcs.SetResult(processIsSuccessful);
            }).ConfigureAwait(false);

            return tcs.Task;
        }
    }
}
