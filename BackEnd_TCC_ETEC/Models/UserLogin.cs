namespace BackEndAplication.Models
{
    public class UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class ReturnLoginModel
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Exception { get; set; }
    }

    public class UserVerificationModel
    {
        public string UserName { get; set; }
    }

    public class UserVerificarionModelReturnUsers
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
