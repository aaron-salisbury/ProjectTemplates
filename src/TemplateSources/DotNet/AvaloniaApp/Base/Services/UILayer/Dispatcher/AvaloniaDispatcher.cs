using Avalonia.Threading;
using System;
using System.Threading.Tasks;

namespace AvaloniaApp.Base.Services;

public class AvaloniaDispatcher : IAgnosticDispatcher
{
    // ref: https://docs.avaloniaui.net/docs/guides/basics/accessing-the-ui-thread

    public async Task<TResult> InvokeOnBackgroundAsync<TResult>(Func<Task<TResult>> action)
    {
        return await Dispatcher.UIThread.InvokeAsync(action, DispatcherPriority.Background);
    }

    public void PostBackground(Action action)
    {
        Dispatcher.UIThread.Post(action, DispatcherPriority.Background);
    }
}
