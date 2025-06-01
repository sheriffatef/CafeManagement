using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class OrderItems : BaseEntity<int>
    {
        public int OrderId { get; set; }


        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Orders Order { get; set; } = null!;
        public Products Product { get; set; } = null!;

    }
}
