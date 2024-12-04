using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CA02_ASP.NET_Core.Data.Entity
{

    [Table("Users")]
    public class UsersEntity
    {
        [Column("user_id"), Key] public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public DateTime created_at { get; set; }
        public string password_hash { get; set; } //for authentication
    }

}
