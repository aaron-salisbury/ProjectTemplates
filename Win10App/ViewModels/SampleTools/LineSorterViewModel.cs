using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
            set => SetProperty(ref _sortTypes, value);
        }

        private ComboBoxEnumItem _selectedSortType;
        public ComboBoxEnumItem SelectedSortType
        {
            get => _selectedSortType;
            set
            {
                SetProperty(ref _selectedSortType, value);
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
                .Select(st => new ComboBoxEnumItem() { Value = (int)st, Text = st.GetAttribute<DisplayAttribute>()?.Name ?? st.ToString() })
                .ToList();

            SelectedSortType = SortTypes
                .Where(cbi => cbi.Value == (int)LineSorter.SelectedSortType)
                .First();
        }
    }
}
