using BackEndAplication.Models;

namespace BackEndAplication.Repositories
{
    public static class UserRepository
    {
        public static Users Get(string username, string password)
        {
            var users = new List<Users>();
            users.Add(new Users { Id = 1, Username = "batman", Password = "batman", Role = "manager" });
            users.Add(new Users { Id = 2, Username = "robin", Password = "robin", Role = "employee" });
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == x.Password).FirstOrDefault();
        }
    }
}
