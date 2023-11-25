using Microsoft.AspNetCore.Hosting.Server;
using MySql.Data.MySqlClient;
using System.Data;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Serilog;

namespace BackEndAplication.Data
{ 
    public class MySQLConnection
    {
        private readonly string _dataBaseSchema = Models.Configuration.GetSectionValue("MySqlServer", "DataBase");
        private readonly string _server = Models.Configuration.GetSectionValue("MySqlServer", "Server");
        private readonly string user = Models.Configuration.GetSectionValue("MySqlServer", "User");
        private readonly string password = Models.Configuration.GetSectionValue("MySqlServer", "Password");

        public string connectionDataBase(string command)
        {
            string connectionString = string.Format("server={0};database={1};uid={2};pwd={3}",
                _server, _dataBaseSchema, user, password);

            var mConn = new MySqlConnection(connectionString);

            try
            {
                mConn.Open();
                MySqlCommand commandExecution = new MySqlCommand(command, mConn);
                Log.Information("[Consulta ao MySql] Executando consulta no banco de dados MySQL.");
                var result = commandExecution.ExecuteNonQuery().ToString();
                return result;
            }
            catch (Exception e)
            {
                Log.Error(string.Format("[Consulta ao MySql] Erro ao realizar consulta: {0}", e.Message));
                return e.Message;
            }
            finally
            {
                mConn.Close();
            }
        }
    }
}
