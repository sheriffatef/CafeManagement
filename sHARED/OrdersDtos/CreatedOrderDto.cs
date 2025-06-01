using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrdersDtos
{
    public class CreatedOrderDto
    {
        public int TableId { get; set; }
        public string GuestName { get; set; } = null!;


        public List<CreatedOrderItemDto> Items { get; set; } = [];
    }
}
