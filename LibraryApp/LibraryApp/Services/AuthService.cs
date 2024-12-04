// SOA CA2 - Matthew & Abiel 
// Front-end App
// JWT Authentification Service Class

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LibraryApp.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", new { email, password });

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
                return result?.Token;
            }

            throw new HttpRequestException("Invalid credentials");
        }
    }

    public class AuthResponse
    {
        public string Token { get; set; }
    }
}

