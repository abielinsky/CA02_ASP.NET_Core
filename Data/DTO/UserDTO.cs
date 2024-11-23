using System.ComponentModel.DataAnnotations;

namespace CA02_ASP.NET_Core.Data.DTO
{
    public class UserDTO
    {


        public int user_id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }


    }
}
