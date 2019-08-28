using System;
using System.Collections.Generic;
using ParcelManagement.Domain.Entities;

namespace ParcelManagement.Departments.Parcels.Services
{
    public interface IParcelsRetrievalService
    {
        List<Parcel> GetParcelsByDepartment(string departmentName);
    }
}
