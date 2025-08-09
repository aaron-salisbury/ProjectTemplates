using DotNet.Business.Modules.Sample.MessageContracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace DotNet.Business.Modules.Sample.DomainServices
{
    public class UUIDGenerator
    {
        private readonly IBus _bus;
        private readonly ILogger _logger;

        public UUIDGenerator(IBus bus, ILogger logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task InitiateAsync(bool shouldCapitalize = true)
        {
            Guid newGuid = Guid.NewGuid();

            string uUID = shouldCapitalize
                ? newGuid.ToString().ToUpper()
                : newGuid.ToString();

            _logger.LogInformation($"Generated new UUID: {uUID}");

            await _bus.Publish<GuidGenerated>(new { UUID = uUID });
        }
    }
}
