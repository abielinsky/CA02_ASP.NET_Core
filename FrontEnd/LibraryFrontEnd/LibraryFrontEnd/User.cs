﻿// Abiel & Matthew SD4 Service Orientated Architecture CA2
// Front End
// User Model

namespace LibraryFrontEnd
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PasswordHash { get; set; }
    }
}
