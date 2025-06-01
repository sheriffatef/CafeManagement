using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.UserDtos;

namespace Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;

        public AuthService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName!),
        new Claim(ClaimTypes.Email, user.Email ?? ""),
        new Claim("Status", user.Status),
        new Claim("JoinedAt", user.JoinedAt.ToString("O")) // optional
    };

            // لو عندك Role، ضيفه برضو:
            var roles = _userManager.GetRolesAsync(user).Result;
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("SuperSefhyjghghcjhkvghjretKey123456"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourapi",
                audience: "yourclient",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<LoginResponsedDto?> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null)
            {
                return null;
            }
            var IsPasswordCorrect = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!IsPasswordCorrect) { return null; }
            var roles = await _userManager.GetRolesAsync(user);

            var token = GenerateToken(user);
            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                role = roles.FirstOrDefault() ?? "user",
                status = user.Status,
                JoinedAt = user.JoinedAt
            };

            return new LoginResponsedDto
            {
                Token = token,
                User = userDto
            };
        }
        public async Task<IdentityResult> RegisterAsync(RegisterDto dto)
        {
            var user = new User
            {
                Email = dto.email,
                UserName = dto.email,
                JoinedAt = DateTime.UtcNow,
                Status = "Active"
            };

            var result = await _userManager.CreateAsync(user, dto.password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
            }

            return result;
        }
    }

}
