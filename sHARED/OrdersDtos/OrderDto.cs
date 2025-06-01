using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrdersDtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; } = null!;
        public decimal total { get; set; }
        public DateTime OrderDate { get; set; }
        public int TableId { get; set; }
        public string GuestName { get; set; } = null!;


        public List<OrderItemsDto> items { get; set; } = [];
    }
}
