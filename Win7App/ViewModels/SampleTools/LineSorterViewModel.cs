using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using Win7App.Base;
using Win7Core.SampleTools;

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

            bool sortLinesFunction() => LineSorter.Initiate();
            ExecuteTaskCommand = new RelayCommand(async () => await InitiateProcessAsync(sortLinesFunction, ExecuteTaskCommand), () => !IsBusy);

            SortTypes = Enum.GetValues(typeof(LineSorter.SortTypes))
                .Cast<LineSorter.SortTypes>()
                .Select(st => new ComboBoxEnumItem() { Value = (int)st, Text = LineSorter.GetSortTypeDisplayName(st) })
                .ToList();

            SelectedSortType = SortTypes
                .Where(cbi => cbi.Value == (int)LineSorter.SelectedSortType)
                .First();
        }
    }
}
