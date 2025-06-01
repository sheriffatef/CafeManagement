using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.OrdersDtos;
using Shared.ProductsDtos;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IServiceManager service) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await service.OrderService.GetAllOrdersAsync();
            return Ok(products);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatedOrderDto dto)
        {

            await service.OrderService.CreateOrderAsync(dto);

            return StatusCode(201);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.OrderService.CancleOrDeleteOrderAsync(id);
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Product = await service.OrderService.GetOrderByIdAsync(id);
            return Product is not null ? Ok(Product) : NotFound();
        }
        [HttpGet("table/{tableId}")]
        public async Task<IActionResult> GetByTableId(int tableId)
        {
            var orders = await service.OrderService.GetOrdersByTableIdAsync(tableId);
            return Ok(orders);
        }
        [HttpPatch("{id}/status")]
        [Authorize] // Requires valid JWT token
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusDto dto)
        {
            var updated = await service.OrderService.UpdateOrderStatusAsync(id, dto.Status);
            return updated ? NoContent() : NotFound();

        }


    }
}
