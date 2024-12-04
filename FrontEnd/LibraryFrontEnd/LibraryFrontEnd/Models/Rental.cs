// Abiel & Matthew SD4 Service Orientated Architecture CA2
// Front End
// Rental Model

namespace LibraryFrontEnd.Models
{
    public class Rental
    {
        public required int id { get; set; }
        public required int user_id { get; set; }
        public required int book_id { get; set; }
        public DateTime rental_date { get; set; }
        public DateTime? return_date { get; set; }
        public string status { get; set; } = string.Empty;
    }
}

