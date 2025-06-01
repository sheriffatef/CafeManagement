using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shared.UserDtos;

namespace ServiceAbstraction
{
    public interface IAuthService
    {
        Task<LoginResponsedDto?> LoginAsync(LoginDto dto);
        Task<IdentityResult> RegisterAsync(RegisterDto dto);
    }
}
