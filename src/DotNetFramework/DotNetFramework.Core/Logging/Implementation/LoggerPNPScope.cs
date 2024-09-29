using System;

namespace DotNetFramework.Core.Logging
{
    public class LoggerPNPScope : IDisposable
    {
        public LoggerPNPScope Parent { get; set; }

        private readonly LoggerPNP _provider;
        private readonly object _state;

        private bool _disposed;

        public LoggerPNPScope(LoggerPNP provider, object state)
        {
            _state = state;

            _provider = provider;
            Parent = provider.CurrentScope;
            _provider.CurrentScope = this;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

                _provider.CurrentScope = Parent;
            }
        }
    }
}
