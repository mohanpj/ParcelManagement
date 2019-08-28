using System;
using ParcelManagement.Domain.Entities;

namespace ParcelManagement.Infrastructure.Repositories
{
    public interface IContainerRepository
    {
        Container GetContainer(string fileName);
    }
}
