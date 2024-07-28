using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WuyiDAL.IReponsitory;
using WuyiDAL.Models;
using WuyiDAL.Models.systems;

namespace WuyiDAL.Repository
{
    public class AuthenticateRepo: IAuthenticateRepo
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticateRepo(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> Register(UserRegistration userRegistration)
        {
            if (await _context.Users.AnyAsync(u => u.Username == userRegistration.Username))
                throw new Exception("Username already exists");

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = userRegistration.Username,
                Email = userRegistration.Email,
                Password = userRegistration.Password, // BCrypt.Net.BCrypt.HashPassword(userRegistration.Password), // Mã hóa mật khẩu phải cài thêm Install-Package BCrypt.Net-Next
                CreatedAt = DateTime.UtcNow,
                Role = 1,
                IsArtist = false
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return GenerateJwtToken(user);
        }

        public async Task<string> Login(UserLogin userLogin)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == userLogin.Username);
            if (user == null || user.Password != userLogin.Password) // if (user == null || !BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password)) nếu mã hóa thì cần giải mã hóa
                throw new Exception("Invalid username or password");

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}
    
