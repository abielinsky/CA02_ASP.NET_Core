using CA02_ASP.NET_Core.Data.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CA02_ASP.NET_Core.Data.Services
{
    // DTO for Login
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public interface ILoginService
    {
        string Login(string username, string password);
    }

    public class LoginService : ILoginService
    {
        Context _context;
        IConfiguration _configuration;
        public LoginService(Context context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }
        public string Login(string username, string password)
        {
            // Find user by email
            var user = _context.Users.FirstOrDefault(u => u.email == username && u.password_hash == password);

            if (user == null) return null;
            if (user.password_hash != password) return null;

            // Generate JWT token
            var token = GenerateJwtToken(user);
            return token;
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
}
