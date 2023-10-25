﻿using AvaloniaApp.Business.SampleTools;
using AvaloniaApp.Presentation.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AvaloniaApp.Presentation.ViewModels
{
    public partial class LineSorterViewModel : BaseViewModel
    {
        public RelayCommand SortCommand { get; }

        [ObservableProperty]
        IEnumerable<LineSorter.SortTypes> _sortTypes;

        [ObservableProperty]
        LineSorter.SortTypes _selectedSortType;

        [ObservableProperty]
        string? _text;

        public LineSorterViewModel()
        {
            _sortTypes = Enum.GetValues(typeof(LineSorter.SortTypes)).Cast<LineSorter.SortTypes>();

            SelectedSortType = SortTypes.First();

            SortCommand = new RelayCommand(Sort);
        }

        private void Sort()
        {
            Text = LineSorter.Initiate(SelectedSortType, Text);
        }
    }
}