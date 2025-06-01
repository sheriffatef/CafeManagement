using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.OrdersDtos;

namespace Presentation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class GuestOrdersController(IServiceManager service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGuestOrderDto dto)
        {
            var orderId = await service.OrderService.CreateGuestOrderAsync(dto);
            return Ok(new { OrderId = orderId });
        }
    }
}

