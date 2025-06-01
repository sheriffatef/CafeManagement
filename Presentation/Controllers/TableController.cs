using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.TablesDtos;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class tablesController(IServiceManager service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tables = await service.tableService.GetAllTablesAsync();
            return Ok(tables);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var table = await service.tableService.GetTableByIdAsync(id);
            return table is not null ? Ok(table) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatedTableDto dto)
        {
            await service.tableService.CreateTableAsync(dto);
            return StatusCode(201); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatedTableDto dto)
        {
            await service.tableService.UpdateTableAsync(id, dto);
            return NoContent();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateTableStatusDto dto)
        {
            await service.tableService.UpdateTableStatusAsync(id, dto.Status);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.tableService.DeleteTableAsync(id);
            return NoContent();
        }
    }
}
 