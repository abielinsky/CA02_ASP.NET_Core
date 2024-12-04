// SOA CA2 - Matthew & Abiel 
// Front-end App
// Api Service Class

using System.Net.Http.Headers;
using System.Text.Json;

namespace LibraryApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private string JwtToken { get; set; }

        public void SetJwtToken(string token)
        {
            JwtToken = token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<string> Login(LoginModel loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", loginModel);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<JwtResponse>();
                return result.Token;
            }
            return null;
        }

        // Ensure this returns a List<Book> as expected
        public async Task<List<Book>> GetBooksAsync()
        {
            // Fetch data from API and deserialize it into a List<Book>
            var books = await _httpClient.GetFromJsonAsync<List<Book>>("api/Books");
            return books;
        }

        public class JwtResponse
        {
            public string Token { get; set; }
        }

        public class LoginModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class Book
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public string ISBN { get; set; }
            public int CopiesAvailable { get; set; }
        }
    }
}
