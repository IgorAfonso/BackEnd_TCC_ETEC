using BackEndAplication.Data;
using System.Runtime.CompilerServices;

namespace BackEndAplication.Services
{
    public class RecoverPasswordServicecs
    {
        public async Task<string> RecoverPassword(string username, string password)
        {
            var getId = string.Format("SELECT ID FROM users WHERE users.username = '{0}'", username);

            var recoveryQuery = string.Format("UPDATE users SET users.Password = '{0}' " +
                "WHERE users.username = '{1}'", password, username);

            var connectionToCreate = new MySQLConnectionWithValue();
            var getIdProcess = connectionToCreate.ValidarExistingUser(getId);

            if (string.IsNullOrEmpty(getIdProcess.Result))
            {
                return "Usuário não existente.";
            }
            else
            {
                var finalResult = connectionToCreate.ValidarExistingUser(recoveryQuery).ToString();
                if (string.IsNullOrEmpty(finalResult))
                {
                    return "Falha ao alterar a senha.";
                }
                else
                {
                    return "Senha alterada com sucesso";
                }
            }
        }
    }
}
