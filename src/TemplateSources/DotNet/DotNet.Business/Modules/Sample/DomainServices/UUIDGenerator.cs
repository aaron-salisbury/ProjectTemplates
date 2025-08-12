using DotNet.Business.Modules.Sample.Events;
using Microsoft.Extensions.Logging;
using RunnethOverStudio.AppToolkit.Modules.Messaging;
using System;
using System.Threading.Tasks;

namespace DotNet.Business.Modules.Sample.DomainServices
{
    public class UUIDGenerator
    {
        private readonly IEventSystem _events;
        private readonly ILogger _logger;

        public UUIDGenerator(IEventSystem eventSystem, ILogger logger)
        {
            _events = eventSystem;
            _logger = logger;
        }

        public async Task InitiateAsync(bool shouldCapitalize = true)
        {
            await Task.Run(() =>
            {
                Guid newGuid = Guid.NewGuid();

                string uUID = shouldCapitalize
                    ? newGuid.ToString().ToUpper()
                    : newGuid.ToString();

                _logger.LogInformation($"Generated new UUID: {uUID}");

                _events.Publish(this, new GuidGenerated() { UUID = uUID });
            });
        }
    }
}
