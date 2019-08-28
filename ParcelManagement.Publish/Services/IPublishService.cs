using System;
using System.Threading.Tasks;
using ParcelManagement.Domain.Entities;

namespace ParcelManagement.Queue.Services
{
    public interface IPublishService
    {
        Task PublishAsync(string queueName, Parcel parcel);
    }
}
