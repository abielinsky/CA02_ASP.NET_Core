using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CA02_ASP.NET_Core.Data.Entity
{

    //public enum RentalStatus
    //{
    //    Borrowed,
    //    Returned
    //}

    [Table("Rentals")]
    public class RentalEntity
    {
        [Column("rental_id"), Key] public int id { get; set; }
        public int user_id { get; set; }

        public int book_id { get; set; }
        [Column(TypeName = "date")] public DateTime rental_date { get; set; }
        [Column(TypeName = "date")] public DateTime? return_date { get; set; }
        public string status { get; set; }
        [ForeignKey("book_id")] public virtual BookEntity Books { get; set; }

    }

}
