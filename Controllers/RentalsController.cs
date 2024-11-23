using CA02_ASP.NET_Core.Data.DTO;
using CA02_ASP.NET_Core.Data.Entity;
using CA02_ASP.NET_Core.Data.Services;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CA02_ASP.NET_Core.Controllers

{

    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : CustomControllerBase<RentalEntity, RentalDTO>
    {
        public RentalsController(IGenericService<RentalEntity> service) : base(service)
        {
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditItem(int id, RentalDTO_update rental)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound("Entity not found");

            //rental.Adapt(entity, typeof(RentalDTO), typeof(RentalEntity));
            entity.return_date = rental.return_date;
            entity.status = "Returned";
            if (entity.return_date == null)
                entity.status = "Borrowed";


            var result = await _service.UpdateAsync(entity);
            if (result > 0)
                return Ok($"Rows affected: {result}");
            return NoContent();
        }
        [HttpGet("test")]
        public async Task<IActionResult> Get_test()        
        {
            var result = await _service.GetAllAsync(null, null, "Books");
            if (result.Count > 0)
                return Ok(result.Adapt<RentalDTO>());
            return NoContent();
        }
    }
}
