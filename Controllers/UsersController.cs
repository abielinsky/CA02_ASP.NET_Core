using CA02_ASP.NET_Core.Data.DTO;
using CA02_ASP.NET_Core.Data.Entity;
using CA02_ASP.NET_Core.Data.Services; // added
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CA02_ASP.NET_Core.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : CustomControllerBase<UsersEntity, UserDTO>
    {
        public UsersController(IGenericService<UsersEntity> service) : base(service)
        {
        }
    }

}
