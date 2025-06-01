using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrdersDtos
{
    public class CreateGuestOrderDto
    {
        public string GuestName { get; set; } = string.Empty;
        public int TableId { get; set; }
        public List<GuestOrderItemDto> OrderItems { get; set; } = new();
    }
}
