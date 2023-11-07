using BackEndAplication.Data;

namespace BackEndAplication.Services
{
    public class CreateUserService
    {
        public async Task<string> CreateUser(string user, string password, string email) 
        {
            var validatorQuery = string.Format(
                "SELECT COUNT(ID) FROM users WHERE username = '{0}'",
                user);
            var createQuery = string.Format(
                "INSERT INTO users(username,email, Password) VALUES('{0}', '{1}', '{2}')",
                user, email, password);

            var connection = new MySQLConnectionWithValue();
            var resultQuery = connection.ValidateExistingUser(validatorQuery);

            if (resultQuery.Result != "userNotFound")
                return "Usuário já existe";

            var connectionToCreate = new MySQLConnection();
            var resultQueryForCreate = connectionToCreate.connectionDataBase(createQuery).ToString();

            if (string.IsNullOrEmpty(resultQueryForCreate))
            {
                return "Fala ao criar usuário";
            }
            else
            {
                return "Usuário Criado com sucesso.";
            }
        }
    }
}
