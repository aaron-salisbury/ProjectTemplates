using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Win7App.Base.Services;

public class WPFDispatcher : DispatcherObject, IAgnosticDispatcher
{
    // ref: https://learn.microsoft.com/en-us/archive/msdn-magazine/2007/october/wpf-threads-build-more-responsive-apps-with-the-dispatcher

    public async Task<TResult> InvokeOnBackgroundAsync<TResult>(Func<Task<TResult>> action)
    {
        return await Dispatcher.Invoke(action, DispatcherPriority.Background);
    }

    public void PostBackground(Action action)
    {
        Dispatcher.Invoke(action, DispatcherPriority.Background, CancellationToken.None);
    }
}
