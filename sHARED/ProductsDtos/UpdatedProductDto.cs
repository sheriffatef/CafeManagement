using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.ProductsDtos
{
    public class UpdatedProductDto
    {
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string Category { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}
