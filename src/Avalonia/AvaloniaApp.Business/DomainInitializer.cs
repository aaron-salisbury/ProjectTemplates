using AvaloniaApp.Data;
using MassTransit;

namespace AvaloniaApp.Business;

public class DomainInitializer
{
    private readonly IBusControl _busControl;
    private readonly DataInitializer _dataInitializer;

    public DomainInitializer(IBusControl busControl, DataInitializer dataInitializer)
    {
        _busControl = busControl;
        _dataInitializer = dataInitializer;
    }

    public async Task StartAsync()
    {
        await _dataInitializer.StartAsync();
        await _busControl.StartAsync();
    }

    public void Shutdown()
    {
        _busControl.Stop();
    }
}
