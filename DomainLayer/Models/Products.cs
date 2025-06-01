using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Products : BaseEntity<int>
    {
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string Category { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;

        public virtual ICollection<OrderItems> OrderItems { get; set; } = [];

    }
}
