using DotNetFrameworkToolkit.Modules.Logging;
using System;

namespace DotNetFramework.Business.Modules.Sample.DomainServices
{
    public class UUIDGenerator
    {
        private readonly ILogger _logger;

        public UUIDGenerator(ILogger logger)
        {
            _logger = logger;
        }

        public string Initiate(bool shouldCapitalize = true)
        {
            Guid newGuid = Guid.NewGuid();

            string uUID = shouldCapitalize
                ? newGuid.ToString().ToUpper()
                : newGuid.ToString();

            LoggerExtensions.LogInformation(_logger, $"Generated new UUID: {uUID}");

            return uUID;
        }
    }
}
