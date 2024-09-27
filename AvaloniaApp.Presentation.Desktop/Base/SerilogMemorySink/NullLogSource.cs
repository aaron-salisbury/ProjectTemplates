using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Serilog.Sinks.MemorySink;

internal class NullLogSource<T> : ILogSource<T>
{
    public event EventHandler<LogEvent>? LogEmitted;

    public void Initialize() { }

    public int GetLogsCount() => 0;

    public Task<IEnumerable<T>> GetLogs(int start, int requiredCount = int.MaxValue, CancellationToken cancellationToken = default) => Task.FromResult<IEnumerable<T>>([]);

    public Task ClearLogs(CancellationToken cancellationToken = default) => Task.CompletedTask;
}
