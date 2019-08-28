using System;
using ParcelManagement.Domain.Entities;
using ParcelManagement.Infrastructure.Utilities;

namespace ParcelManagement.Infrastructure.Repositories
{
    public class ContainerRepository : IContainerRepository
    {
        public ContainerRepository()
        {
        }

        public Container GetContainer(string fileName)
        {
            Container containerInfo = null;
            try
            {
                containerInfo = XmlHelper.DeserializeFromXmlFile<Container>(fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                Console.WriteLine(ex.Message);
            }
            return containerInfo;
        }
    }
}
