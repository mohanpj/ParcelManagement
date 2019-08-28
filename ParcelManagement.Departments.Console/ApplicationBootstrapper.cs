using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ParcelManagement.Departments.Parcels.Services;

namespace ParcelManagement.Departments.Parcels
{
    public class ApplicationBootstrapper
    {
        private readonly ILogger<ApplicationBootstrapper> _logger;
        private readonly IParcelsRetrievalService _parcelsRetrievalService;

        public ApplicationBootstrapper(
            ILogger<ApplicationBootstrapper> logger,
            IParcelsRetrievalService parcelsRetrievalService)
        {
            _logger = logger;
            _parcelsRetrievalService = parcelsRetrievalService;
        }

        public void Run()
        {
            try
            {
                List<string> departments = new List<string> { "insurance", "mail", "regular", "heavy" };
                foreach (var deparment in departments)
                {
                    var parcels = _parcelsRetrievalService.GetParcelsByDepartment(deparment);
                    if (parcels?.Count > 0)
                    {
                        Console.WriteLine(JsonConvert.SerializeObject(parcels));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
            }
        }
    }
}
