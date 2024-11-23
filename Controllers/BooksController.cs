using CA02_ASP.NET_Core.Data.DTO;
using CA02_ASP.NET_Core.Data.Entity;

using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace CA02_ASP.NET_Core.Controllers

{


    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : CustomControllerBase<BookEntity, BookDTO>
    {
        public BooksController(IGenericService<BookEntity> service) : base(service)
        {
        }
    }


}
