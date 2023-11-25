using System.ComponentModel.DataAnnotations;

namespace BackEndAplication.Models
{
    public class Users
    {
        public int Id { get; set; } = 0;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
    }

    public class CreateUserId
    {
        public int Id { get; set;}
    }

    public class UserGetInformations
    {
        public int Id { get; set;}
        public string Username { get; set; }
        public string Email { get; set; }
        public string LastPeriod { get; set; }
    }
}
