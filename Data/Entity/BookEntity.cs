using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CA02_ASP.NET_Core.Data.Entity
{

    [Table("Books")]
    public class BookEntity
    {
        [Column("book_id"), Key] public int id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string isbn { get; set; }
        public int copies_available { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;

        public virtual IEnumerable<RentalEntity> Rentals { get; set; }

    }

}
