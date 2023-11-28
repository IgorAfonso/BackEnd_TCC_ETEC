using BackEndAplication.Data;
using MySql.Data.MySqlClient;

namespace BackEnd_TCC_ETEC.Services
{
    public class DeleteUserService
    {
        public string DeleteUser(string ID)
        {
            var queryUsersTable = string.Format("delete from users where users.ID = '{0}'", ID);
            var queryOperationTable = string.Format("delete from operations where operations.IDUser = '{0}'", ID);
            var queryAdressTable = string.Format("delete from adress where adress.IDUser = '{0}'", ID);

            var myConn = new MySQLConnection();

            var executionDeleteUsers = myConn.connectionDataBase(queryUsersTable);
            var executionDeleteOperations = myConn.connectionDataBase(queryOperationTable);
            var executionDeleteAdress = myConn.connectionDataBase(queryAdressTable);

            if (string.IsNullOrEmpty(executionDeleteUsers) || string.IsNullOrEmpty(executionDeleteOperations) 
                || string.IsNullOrEmpty(executionDeleteAdress))
            {
                return "Falha ao deletar usuário usuário";
            }
            else
            {
                return "Usuário Deletado com sucesso.";
            }
        }
    }
}
