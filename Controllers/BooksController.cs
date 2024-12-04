using CA02_ASP.NET_Core.Data.DTO;
using CA02_ASP.NET_Core.Data.Entity;
using CA02_ASP.NET_Core.Data.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CA02_ASP.NET_Core.Controllers

{
    [Route("api/[controller]"), Authorize, ApiController]
    public class BooksController : CustomControllerBase<BookEntity, BookDTO>
    {
        public BooksController(IGenericService<BookEntity> service) : base(service)
        {
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetBooksByName(string Name)
        {
            var books = await _service.GetAllAsync(x => x.title.Contains(Name), null, "Rentals");

            //var response = books.Select(x => new BookDTO
            //{
            //    title = x.title,
            //    author = x.author,
            //    isbn = x.isbn,
            //    copies_available = x.copies_available
            //}).ToList();
            var response = books.Adapt<List<BookDTO>>();
            return Ok(response);
        }
    }


}
