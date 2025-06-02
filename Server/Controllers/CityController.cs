using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Library.Helpers;
using Server.Library.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController(IGenericRepositoryInterface<City> genericRepositoryInterface)
    : GenericController<City>(genericRepositoryInterface)
    {
        [HttpGet("cities-excel")]
        public IActionResult GetCitiesExcel()
        {
            var cities = new List<City>
                {
                    new City { Id = 1, Name = "Paris", Country = new Country { Name = "France" } },
                    new City { Id = 2, Name = "New York", Country = new Country { Name = "USA" } }
                };

            var fileContent = ExcelExportHelper.ExportToExcel(cities);

            return File(fileContent,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Cities.xlsx");
        }
    }
}
