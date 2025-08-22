using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DotNet.Business.Modules.Sample.ApplicationServices;
using DotNet.Business.Modules.Sample.DomainServices;
using DotNet.Business.Modules.Sample.Events;
using RunnethOverStudio.AppToolkit.Modules.Messaging;
using RunnethOverStudio.AppToolkit.Presentation.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvaloniaApp.ViewModels;

public partial class LineSorterViewModel : BaseViewModel
{
    public IAsyncRelayCommand SortCommand { get; }

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
    }

    private readonly ISampleToolsService _sampleToolsService;

    public LineSorterViewModel(ISampleToolsService sampleToolsService, IEventSystem eventSystem)
    {
        _sortTypes = Enum.GetValues(typeof(LineSorter.SortTypes)).Cast<LineSorter.SortTypes>();
        _selectedSortTypeIndex = (int)_sortTypes.First();
        _sampleToolsService = sampleToolsService;

        SortCommand = new AsyncRelayCommand(SortAsync);

        eventSystem.Subscribe<TextSorted>(OnTextSorted);
    }

    private async Task SortAsync()
    {
        await _sampleToolsService.InitializeLineSortingAsync((LineSorter.SortTypes)SelectedSortTypeIndex, Text);
    }

    private void OnTextSorted(object? sender, TextSorted e)
    {
        Text = e.SortedText;
    }
}
