using AvaloniaApp.Business.Modules.Sample.ApplicationServices;
using AvaloniaApp.Business.Modules.Sample.MessageContracts;
using AvaloniaApp.Presentation.Desktop.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MassTransit;
using System.Threading.Tasks;

namespace AvaloniaApp.Presentation.Desktop.ViewModels;

public partial class UUIDGeneratorViewModel : BaseViewModel, IConsumer<GuidGenerated>
{
    public IAsyncRelayCommand GenerateCommand { get; }

    [ObservableProperty]
    bool _capitalize;

    [ObservableProperty]
    string? _uUID;

    private readonly ISampleToolsService _sampleToolsService;

    public UUIDGeneratorViewModel(ISampleToolsService sampleToolsService)
    {
        _sampleToolsService = sampleToolsService;

        GenerateCommand = new AsyncRelayCommand(GenerateUUIDAsync);
    }

    private async Task GenerateUUIDAsync()
    {
        await _sampleToolsService.InitializeGUIDGenerationAsync(Capitalize);
    }

    public Task Consume(ConsumeContext<GuidGenerated> context)
    {
        UUID = context.Message.UUID;

        return Task.CompletedTask;
    }
}
