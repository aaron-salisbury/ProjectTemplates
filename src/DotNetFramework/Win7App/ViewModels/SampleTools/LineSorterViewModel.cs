using DotNetFramework.Business.Modules.Sample.ApplicationServices;
using DotNetFramework.Business.Modules.Sample.DomainServices;
using DotNetFramework.Core.ExtensionHelpers;
using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using Win7App.Base;
using Win7App.Base.Services;

namespace Win7App.ViewModels.SampleTools
{
    public class LineSorterViewModel : BaseViewModel
    {
        public RelayCommand ExecuteTaskCommand { get; }

        private List<ComboBoxEnumItem> _sortTypeItems;
        public List<ComboBoxEnumItem> SortTypeItems
        {
            get { return _sortTypeItems; }
            set
            {
                _sortTypeItems = value;
                RaisePropertyChanged(nameof(SortTypeItems));
            }
        }

        private int _selectedSortTypeIndex;
        public int SelectedSortTypeIndex
        {
            get => _selectedSortTypeIndex;
            set => SetField(ref _selectedSortTypeIndex, value, nameof(SelectedSortTypeIndex));
        }

        string _text;
        public string Text
        {
            get => _text;
            set => SetField(ref _text, value, nameof(Text));
        }

        private readonly ISampleToolsService _sampleToolsService;
        private readonly List<LineSorter.SortTypes> _sortTypes;

        public LineSorterViewModel(ISampleToolsService sampleToolsService, IAgnosticDispatcher dispatcher)
        {
            _sampleToolsService = sampleToolsService;
            ExecuteTaskCommand = new RelayCommand(async (object o) => await InitiateLongRunningProcessAsync(Sort, dispatcher), (object o) => !IsBusy);
            
            _sortTypes = Enum.GetValues(typeof(LineSorter.SortTypes))
                .Cast<LineSorter.SortTypes>()
                .ToList();

            _sortTypeItems = _sortTypes
                .Select(st => new ComboBoxEnumItem() { Value = (int)st, Text = StringExtensions.SplitPascalCase(st.ToString()) })
                .ToList();

            _selectedSortTypeIndex = 0;
        }

        private bool Sort()
        {
            string sortedText = _sampleToolsService.InitializeLineSorting(_sortTypes[SelectedSortTypeIndex], Text);

            Text = sortedText;

            return sortedText != null;
        }
    }
}
