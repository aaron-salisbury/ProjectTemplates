using System;
using System.Threading.Tasks;

namespace AvaloniaApp.Presentation.Desktop.Base.Services;

public interface IAgnosticDispatcher
{
    /// <summary>
    /// Executes the specified Func<Task<TResult>> asynchronously on the thread that the Dispatcher was created on.
    /// </summary>
    Task<TResult> InvokeOnBackgroundAsync<TResult>(Func<Task<TResult>> action);

    /// <summary>
    /// Posts an action that will be invoked on the dispatcher thread.
    /// </summary>
    void PostBackground(Action action);
}
