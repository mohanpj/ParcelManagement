using System;
using Microsoft.Extensions.Logging;
using ParcelManagement.Distribution.Handlers;

namespace ParcelManagement.Distribution
{
    public class ApplicationBootstrapper
    {
        private readonly ILogger<ApplicationBootstrapper> _logger;
        private readonly IDistributeParcelsHandler _distributeParcelsHandler;

        public ApplicationBootstrapper(
            ILogger<ApplicationBootstrapper> logger,
            IDistributeParcelsHandler distributeParcelsHandler)
        {
            _logger = logger;
            _distributeParcelsHandler = distributeParcelsHandler;
        }

        public void Run()
        {
            try
            {
                _distributeParcelsHandler.Handle();
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
            }
        }
    }
}
