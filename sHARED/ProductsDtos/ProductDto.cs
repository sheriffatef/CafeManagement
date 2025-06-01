using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ProductsDtos
{
    public class ProductsDto
    {
        public int id { get; set; }
        public string name { get; set; } = null!;
        public string description { get; set; } = null!;
        public decimal price { get; set; }
        public string category { get; set; } = null!;
        public string imageUrl { get; set; } = null!;

    }
}
