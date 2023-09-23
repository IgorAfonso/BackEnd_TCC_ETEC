using Microsoft.AspNetCore.Hosting.Server;
using MySql.Data.MySqlClient;
using System.Data;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace BackEndAplication.Data
{ 
    public class MySQLConnection
    {
        private string _dataBaseSchema;
        private string _server;
        private string user;
        private string password;

        public string connectionDataBase(string command)
        {
            _dataBaseSchema = Models.Configuration.GetSectionValue("MySqlServer", "DataBase");
            _server = Models.Configuration.GetSectionValue("MySqlServer", "Server");
            user = Models.Configuration.GetSectionValue("MySqlServer", "User");
            password = Models.Configuration.GetSectionValue("MySqlServer", "Password");

            string connectionString = string.Format("server={0};database={1};uid={2};pwd={3}",
                _server, _dataBaseSchema, user, password);

            var mConn = new MySqlConnection(connectionString);

            try
            {
                mConn.Open();
                try
                {
                    MySqlCommand commandExecution = new MySqlCommand(command, mConn);
                    var result = commandExecution.ExecuteNonQuery().ToString();
                    return result;
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                mConn.Close();
            }
        }
    }
}
