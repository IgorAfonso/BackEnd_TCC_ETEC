using BackEndAplication.Data;

namespace BackEndAplication.Services
{
    public class CreateUserService
    {
        public async Task<string> CreateUser(string user, string password, string email) 
        {
            var userJSON = user;
            var passwordJSON = password;
            var emailJSON = email;
            var query = string.Format("SP_AESENCRYPT_USER_API({0}, {1}, {2}",
                userJSON, passwordJSON, emailJSON);

            MySQLConnection executorQuery = new MySQLConnection();
            string resultQuery = executorQuery.connectionDataBase(query);

            return resultQuery;
        }
    }
}
