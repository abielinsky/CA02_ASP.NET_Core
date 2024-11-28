////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////

using Microsoft.AspNetCore.Mvc;
using CA02_ASP.NET_Core.Data.Services;

namespace CA02_ASP.NET_Core.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class AuthController : ControllerBase
    {
        ILoginService _loginService;
        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        // Login Endpoint
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            var result = _loginService.Login(login.Email, login.Password);
            if (result == null) return Unauthorized("Invalid login attempt");

            return Ok(new { Token = result });
        }
    }

}
