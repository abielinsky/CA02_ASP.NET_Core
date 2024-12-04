// Abiel & Matthew SD4 Service Orientated Architecture CA2
// Front End
// User Model

namespace LibraryFrontEnd.Models
{
    public class User
    {
        public required int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
    }
}

