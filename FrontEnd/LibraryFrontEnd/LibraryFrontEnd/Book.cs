// Abiel & Matthew SD4 Service Orientated Architecture CA2
// Front End
// Book Model

namespace LibraryFrontEnd
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int CopiesAvailable { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
