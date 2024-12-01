// Abiel & Matthew SD4 Service Orientated Architecture CA2
// Front End
// Library Api Service Class

using RestSharp;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibraryFrontEnd.Services
{
    public class LibraryService
    {
        private const string ApiBaseUrl = "API KEY"; // Api still not fully deployed yet

        // Fetch methods for each table
        // Fetch all users
        public async Task<List<User>> GetAllUsersAsync()
        {
            var client = new RestClient($"{ApiBaseUrl}/users");
            var request = new RestRequest();
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful && response.Content != null)
            {
                return JsonSerializer.Deserialize<List<User>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return new List<User>();
        }

        // Fetch all books
        public async Task<List<Book>> GetAllBooksAsync()
        {
            var client = new RestClient($"{ApiBaseUrl}/books");
            var request = new RestRequest();
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful && response.Content != null)
            {
                return JsonSerializer.Deserialize<List<Book>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return new List<Book>();
        }

        // Fetch all rentals
        public async Task<List<Rental>> GetAllRentalsAsync()
        {
            var client = new RestClient($"{ApiBaseUrl}/rentals");
            var request = new RestRequest();
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful && response.Content != null)
            {
                return JsonSerializer.Deserialize<List<Rental>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return new List<Rental>();
        }
    }
}
