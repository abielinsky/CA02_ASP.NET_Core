using Microsoft.AspNetCore.Antiforgery;

namespace CA02_ASP.NET_Core.Data.DTO
{
    public class BookDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string isbn { get; set; }
        public int copies_available { get; set; }
        public IEnumerable<RentalDTO> Rentals { get; set; }
    }
}
