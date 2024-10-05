using DotNet.Business.Modules.Sample.ApplicationServices;
using DotNet.Business.Modules.Sample.DomainServices;
using DotNet.Business.Modules.Sample.MessageContracts;
using AvaloniaApp.Presentation.Desktop.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MassTransit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvaloniaApp.Presentation.Desktop.ViewModels;

public partial class LineSorterViewModel : BaseViewModel, IConsumer<TextSorted>
{
    public RelayCommand ExecuteTaskCommand { get; }
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

    public LineSorterViewModel(ISampleToolsService sampleToolsService)
    {
        _sortTypes = Enum.GetValues(typeof(LineSorter.SortTypes)).Cast<LineSorter.SortTypes>();
        _selectedSortTypeIndex = (int)_sortTypes.First();
        _sampleToolsService = sampleToolsService;

        SortCommand = new AsyncRelayCommand(SortAsync);
    }

    private async Task SortAsync()
    {
        await _sampleToolsService.InitializeLineSortingAsync((LineSorter.SortTypes)SelectedSortTypeIndex, Text);
    }

    public Task Consume(ConsumeContext<TextSorted> context)
    {
        Text = context.Message.SortedText;

        return Task.CompletedTask;
    }
}
