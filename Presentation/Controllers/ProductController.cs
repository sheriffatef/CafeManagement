using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.ProductsDtos;
using DomainLayer.Models;
using System.Security.Claims;
namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class productsController(IServiceManager service, UserManager<User> userManager) : ControllerBase
    {
        [AllowAnonymous]

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await service.productService.GetAllProductsAsync();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);
            Console.WriteLine(user);
            return Ok(products);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatedProductsDto dto)
        {
            await service.productService.CreateProductsAsync(dto);
            return StatusCode(201);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Product = await service.productService.GetProductByIdAsync(id);
            return Product is not null ? Ok(Product) : NotFound();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatedProductDto dto)
        {
            await service.productService.UpdateProductAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.productService.DeleteProductAsync(id);
            return NoContent();
        }
        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var products = await service.productService.GetProductBycategoryAsync(category);
            return Ok(products);
        }
    }
}
