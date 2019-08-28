using System;
using System.Collections.Generic;
using ParcelManagement.Domain.Entities;

namespace ParcelManagement.Departments.Api.Services
{
    public interface IParcelsRetrievalService
    {
        List<Parcel> GetParcelsByDepartment(string departmentName);
    }
}
