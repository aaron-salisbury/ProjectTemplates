using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Serilog.Sinks.MemorySink;

public interface ILogSource<T>
{
    event EventHandler<LogEvent>? LogEmitted;

    void Initialize();

    int GetLogsCount();

    Task<IEnumerable<T>> GetLogs(int start, int requiredCount = int.MaxValue, CancellationToken cancellationToken = default);

    Task ClearLogs(CancellationToken cancellationToken = default);
}
