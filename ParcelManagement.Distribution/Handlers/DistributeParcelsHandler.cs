using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using ParcelManagement.Domain.Entities;
using ParcelManagement.Infrastructure.Repositories;
using ParcelManagement.Queue.Services;

namespace ParcelManagement.Distribution.Handlers
{
    public class DistributeParcelsHandler : IDistributeParcelsHandler
    {
        private readonly ILogger<DistributeParcelsHandler> _logger;
        private readonly IContainerRepository _containerRepository;
        private readonly IPublishService _publishService;

        public DistributeParcelsHandler(
            ILogger<DistributeParcelsHandler> logger,
            IContainerRepository containerRepository,
            IPublishService publishService)
        {
            _logger = logger;
            _containerRepository = containerRepository;
            _publishService = publishService;
        }

        public void Handle()
        {
            var path = GetFilePath();
            Container containerInfo = _containerRepository.GetContainer(path);
            if (containerInfo?.Parcels?.Parcel?.Count > 0)
            {
                _logger.LogDebug(containerInfo.ToString());

                this.HandleInsuranceSignOffParcels(containerInfo);
                this.HandleMailParcels(containerInfo);
                this.HandleRegularParcels(containerInfo);
                this.HandleHeavyParcels(containerInfo);
            }
            
        }

        public void HandleInsuranceSignOffParcels(Container containerInfo)
        {
            var insuranceSignOffParcels = containerInfo.Parcels.Parcel.Where(p => p.Value > 1000).ToList();
            string insuranceSignOffParcelsQueue = "parcels.department.insurance";
            if (insuranceSignOffParcels != null)
            {
                foreach (var parcel in insuranceSignOffParcels)
                {
                    _publishService.PublishAsync(insuranceSignOffParcelsQueue, parcel);
                }
            }
        }

        public void HandleMailParcels(Container containerInfo)
        {
            var mailParcels = containerInfo.Parcels.Parcel.Where(p => p.Weight <= 1 && p.Value <= 1000).ToList();
            string mailParcelsQueue = "parcels.department.mail";
            if (mailParcels != null)
            {
                foreach (var parcel in mailParcels)
                {
                    _publishService.PublishAsync(mailParcelsQueue, parcel);
                }
            }
        }

        public void HandleRegularParcels(Container containerInfo)
        {
            var regularParcels = containerInfo.Parcels.Parcel.Where(p => p.Weight <= 10 && p.Value <= 1000).ToList();
            string regularParcelsQueue = "parcels.department.regular";
            if (regularParcels != null)
            {
                foreach (var parcel in regularParcels)
                {
                    _publishService.PublishAsync(regularParcelsQueue, parcel);
                }
            }
        }

        public void HandleHeavyParcels(Container containerInfo)
        {
            var heavyParcels = containerInfo.Parcels.Parcel.Where(p => p.Weight > 10 && p.Value <= 1000).ToList();
            string heavyParcelsQueue = "parcels.department.heavy";
            if (heavyParcels != null)
            {
                foreach (var parcel in heavyParcels)
                {
                    _publishService.PublishAsync(heavyParcelsQueue, parcel);
                }
            }
        }

        public string GetFilePath()
        {
            string fileName = "Container_68465468.xml";
            string path = Path.Combine(Environment.CurrentDirectory, @"Data", fileName);
            return path;
        }
    }
}
