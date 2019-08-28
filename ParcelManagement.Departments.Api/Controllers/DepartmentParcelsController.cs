using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParcelManagement.Departments.Api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParcelManagement.Departments.Api.Controllers
{
    [Route("api/[controller]")]
    public class DepartmentParcelsController : Controller
    {
        private readonly IParcelsRetrievalService _parcelsRetrievalService;

        public DepartmentParcelsController(IParcelsRetrievalService parcelsRetrievalService)
        {
            _parcelsRetrievalService = parcelsRetrievalService;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Use department value [insurance, mail, regular, heavy] in the url to get parcels" };
        }

        // GET api/departmentparcels/mail
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            string departmentName = id;
            var parcels = _parcelsRetrievalService.GetParcelsByDepartment(departmentName);
            if (!parcels.Any())
            {
                return NotFound(departmentName);
            }
            return Json(new { department = departmentName, parcels = JsonConvert.SerializeObject(parcels) });
        }
    }
}
