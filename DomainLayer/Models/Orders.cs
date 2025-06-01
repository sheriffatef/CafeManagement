using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Orders : BaseEntity<int>
    {
        public int TableId { get; set; }
        public int? UserId { get; set; }
        public string GuestName { get; set; } = null!;
        public string Status { get; set; } = "new";
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual User? User { get; set; } = null!;
        public virtual Tables Table { get; set; } = null!;
        public virtual ICollection<OrderItems> OrderItems { get; set; } = [];



    }
}
