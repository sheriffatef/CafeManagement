using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Tables : BaseEntity<int>
    {
        public string TableName { get; set; } = null!;
        public int Capacity { get; set; }
        public string Status { get; set; } = null!;
        public virtual ICollection<Orders> Orders { get; set; } = [];



    }
}
