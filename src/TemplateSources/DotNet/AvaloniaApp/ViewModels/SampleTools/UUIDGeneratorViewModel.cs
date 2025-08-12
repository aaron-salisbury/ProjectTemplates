using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DotNet.Business.Modules.Sample.ApplicationServices;
using DotNet.Business.Modules.Sample.Events;
using RunnethOverStudio.AppToolkit.Modules.Messaging;
using RunnethOverStudio.AppToolkit.Presentation.MVVM;
using System.Threading.Tasks;

namespace AvaloniaApp.Presentation.Desktop.ViewModels;

public partial class UUIDGeneratorViewModel : BaseViewModel
{
    public IAsyncRelayCommand GenerateCommand { get; }

    [ObservableProperty]
    bool _capitalize;

    [ObservableProperty]
    string? _uUID;

    private readonly ISampleToolsService _sampleToolsService;

    public UUIDGeneratorViewModel(ISampleToolsService sampleToolsService, IEventSystem eventSystem)
    {
        _sampleToolsService = sampleToolsService;

        GenerateCommand = new AsyncRelayCommand(GenerateUUIDAsync);

        eventSystem.Subscribe<GuidGenerated>(OnGuidGenerated);
    }

    private async Task GenerateUUIDAsync()
    {
        await _sampleToolsService.InitializeGUIDGenerationAsync(Capitalize);
    }

    private void OnGuidGenerated(object? sender, GuidGenerated e)
    {
        UUID = e.UUID;
    }
}
