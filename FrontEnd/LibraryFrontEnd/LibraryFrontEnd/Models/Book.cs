// Abiel & Matthew SD4 Service Orientated Architecture CA2
// Front End
// Book Model

namespace LibraryFrontEnd.Models
{
    public class Book
    {
        public required int id { get; set; }
        public required string title { get; set; }
        public required string author { get; set; }
        public required string isbn { get; set; }
        public required int copies_available { get; set; }
        public DateTime created_at { get; set; }
    }
}

