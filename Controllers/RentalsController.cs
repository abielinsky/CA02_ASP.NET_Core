using CA02_ASP.NET_Core.Data.DTO;
using CA02_ASP.NET_Core.Data.Entity;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CA02_ASP.NET_Core.Controllers

{

    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : CustomControllerBase<RentalEntity, RentalDTO>
    {
        public RentalsController(IGenericService<RentalEntity> service) : base(service)
        {
        }
    }

}
