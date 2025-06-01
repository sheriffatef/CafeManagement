using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DomainLayer.Models
{
    public class User : IdentityUser
    {
        public string Status { get; set; } = null!;
        public DateTime JoinedAt { get; set; }

        public virtual ICollection<Orders> Orders { get; set; } = [];

    }
}
