// Abiel & Matthew SD4 Service Orientated Architecture CA2
// Front End
// Rental Model

namespace LibraryFrontEnd
{
    public class Rental
    {
        public int RentalId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; }
    }
}
