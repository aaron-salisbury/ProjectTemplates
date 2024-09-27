using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using System;

namespace Serilog.Sinks.MemorySink;

internal static class LoggerSinkConfigurationExtensions
{
    public static LoggerConfiguration MemorySink<T>(this LoggerSinkConfiguration config, out ILogSource<T> logEventSource, Action<MemorySinkOptions<T>>? options = null)
    {
        var memorySinkOptions = new MemorySinkOptions<T>();
        options?.Invoke(memorySinkOptions);

        if (typeof(T) != typeof(LogEvent) &&
            memorySinkOptions.LogEventConverter is null)
        {
            throw new ArgumentException("'LogEventConverter' is required when T is not type of LogEvent.");
        }

        var sink = new MemorySink<T>(memorySinkOptions);
        logEventSource = sink;
        sink.Initialize();
        return config.Sink(sink);
    }
}
