using CA02_ASP.NET_Core.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CA02_ASP.NET_Core.Data.DTO
{
    public class RentalDTO_update
    {
        public DateTime? return_date { get; set; }
    }

    public class RentalDTO
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int book_id { get; set; }
        public DateTime rental_date { get; set; }
        public DateTime? return_date { get; set; }
        public string status { get; set; }

        public BookDTO? Book { get; set; }

    }
}
