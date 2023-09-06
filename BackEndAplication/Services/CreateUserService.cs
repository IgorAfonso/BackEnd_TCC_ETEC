using BackEndAplication.Data;

namespace BackEndAplication.Services
{
    public class CreateUserService
    {
        public async Task<string> CreateUser(string user, string password, string email) 
        {
            var query = string.Format(
                "INSERT INTO USERS (NAME, EMail, Password) VALUES ({0}, {1}, {2})",
                user, email, password);

            MySQLConnection connection = new MySQLConnection();
            var resultQuery = connection.connectionDataBase(query);

            return resultQuery;
        }
    }
}
