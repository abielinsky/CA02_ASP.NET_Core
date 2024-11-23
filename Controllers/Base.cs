using CA02_ASP.NET_Core.Data.DTO;
using CA02_ASP.NET_Core.Data.Entity;
using CA02_ASP.NET_Core.Data.Services;

using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CA02_ASP.NET_Core.Controllers

{

    public class CustomControllerBase<EntityType, DTOType> : ControllerBase
       where EntityType : class
       where DTOType : class
    {
        public readonly IGenericService<EntityType> _service;
        public CustomControllerBase(IGenericService<EntityType> service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            var response = result.Adapt<List<DTOType>>();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result.Adapt<EntityType>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> NewItem(DTOType book)
        {
            var result = await _service.AddAsync(book.Adapt<EntityType>());
            if (result > 0)
                return Created("", book);
            return NoContent();
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> EditItem(int id, DTOType book)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound("Entity not found");

            book.Adapt(entity, typeof(DTOType), typeof(EntityType));

            var result = await _service.UpdateAsync(entity);
            if (result > 0)
                return Ok($"Rows affected: {result}");
            return NoContent();
        }

        [HttpDelete]
        public virtual async Task<IActionResult> DeleteItem(int id)
        {
            var result = await _service.DeleteByIdAsync(id);
            if (result > 0)
                return Ok($"Rows affected: {result}");
            return NoContent();
        }   
    }


}
