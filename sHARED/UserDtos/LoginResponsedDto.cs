using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.UserDtos
{
    public class LoginResponsedDto
    {

        public string Token { get; set; } = null!;
        public UserDto User { get; set; } = null!;
    }
}
