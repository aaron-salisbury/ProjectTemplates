using AvaloniaApp.Business.SampleTools;
using AvaloniaApp.Presentation.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AvaloniaApp.Presentation.ViewModels
{
    public partial class LineSorterViewModel : BaseViewModel
    {
        public RelayCommand SortCommand { get; }

        [ObservableProperty]
        IEnumerable<LineSorter.SortTypes> _sortTypes;

        [ObservableProperty]
        int _selectedSortTypeIndex;

        string? _text;
        [Required]
        public string? Text
        {
            get => _text;
            set => SetProperty(ref _text, value, true);
            //   Validation:
            // https://docs.avaloniaui.net/docs/next/guides/development-guides/data-validation
            // https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/observablevalidator
        }

        public LineSorterViewModel()
        {
            _sortTypes = Enum.GetValues(typeof(LineSorter.SortTypes)).Cast<LineSorter.SortTypes>();

            _selectedSortTypeIndex = (int)SortTypes.First();

            SortCommand = new RelayCommand(Sort);
        }

        private void Sort()
        {
            Text = LineSorter.Initiate((LineSorter.SortTypes)SelectedSortTypeIndex, Text);
        }
    }
}
