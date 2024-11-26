using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CA02_ASP.NET_Core.Data.Entity;
using CA02_ASP.NET_Core.Data;
using BCrypt.Net;
using Org.BouncyCastle.Crypto.Generators;
using CA02_ASP.NET_Core.Controllers;
using CA02_ASP.NET_Core.Data.DTO;
using Moq;
using Xunit;

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

            // Plain password validation (NO HASHING)
            if (user.password_hash != login.Password)
            {
                return Unauthorized("Invalid login attempt");
            }

            // Generate JWT token
            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }







        private string GenerateJwtToken(UsersEntity user)
        {
            // Retrieve and validate configuration values
            var key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var expiryInMinutes = _configuration["Jwt:TokenExpiryInMinutes"];

            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || string.IsNullOrEmpty(expiryInMinutes))
            {
                throw new InvalidOperationException("JWT configuration values are missing or invalid.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Add claims
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
        new Claim(ClaimTypes.Email, user.email)
    };

            // Generate token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(expiryInMinutes)),
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
