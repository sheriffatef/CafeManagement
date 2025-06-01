using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.UserDtos
{
    public class RegisterDto
    {
        public string name { get; set; } = null!;
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
        public string confirmPassword { get; set; } = null!;

    }
}
