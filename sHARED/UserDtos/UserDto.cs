using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.UserDtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string role { get; set; } = null!;
        public string status { get; set; } = null!;
        public DateTime JoinedAt { get; set; }

    }
}
