using System.ComponentModel.DataAnnotations;

namespace BackEndAplication.Models
{
    public class UserLogin
    {
        [MaxLength(50)]
        public string UserName { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
    }

    public class ReturnLoginModel
    {
        [MaxLength(50)]
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Exception { get; set; }
    }

    public class UserVerificationModel
    {
        [MaxLength(50)]
        public string UserName { get; set; }
    }

    public class UserVerificarionModelReturnUsers
    {
        [MaxLength(50)]
        public string UserName { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
    }
}
