using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace CA02_ASP.NET_Core.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiKeyMiddleware> _logger;

        public ApiKeyMiddleware(RequestDelegate next, ILogger<ApiKeyMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // Method for checking the API key
        public async Task InvokeAsync(HttpContext context)
        {
            // Skip for swagger... for local testing
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            // Check API key in the header
            if (!context.Request.Headers.ContainsKey("APIKey") ||
                context.Request.Headers["APIKey"] != "SuperSecretApiKey12345")
            {
                _logger.LogWarning("Invalid or missing API Key.");
                context.Response.StatusCode = 403;  // Forbidden status code
                await context.Response.WriteAsync("Forbidden: Invalid API Key.");
                return;
            }

            await _next(context); 
        }
    }
}
