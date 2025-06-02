using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Library.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationController(IGenericRepositoryInterface<Vacation> genericRepositoryInterface)
    : GenericController<Vacation>(genericRepositoryInterface)
    {
    }
}
