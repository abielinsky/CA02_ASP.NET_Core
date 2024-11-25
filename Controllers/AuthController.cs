using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CA02_ASP.NET_Core.Data.Entity;
using CA02_ASP.NET_Core.Data;
using BCrypt.Net;
using Org.BouncyCastle.Crypto.Generators;

namespace CA02_ASP.NET_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Context _context;

        public AuthController(IConfiguration configuration, Context context)
        {
            _configuration = configuration;
            _context = context;
        }

        // Login Endpoint
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            // Find user by email
            var user = _context.Users.FirstOrDefault(u => u.email == login.Email);

            if (user == null)
            {
                return Unauthorized("Invalid login attempt");
            }

            // Correct password validation using BCrypt
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(login.Password, user.password_hash);

            if (!isPasswordValid)
            {
                return Unauthorized("Invalid login attempt");
            }

            // Generate JWT token
            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(UsersEntity user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Email, user.email)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:TokenExpiryInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    // DTO for Login
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
