using Microsoft.AspNetCore.Hosting.Server;
using MySql.Data.MySqlClient;
using System.Data;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace BackEndAplication.Data
{ 
    public class MySQLConnection
    {
        public string connectionDataBase(string command)
        {

            var AppName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string dataBaseName = "fastcarddb"; //AppName.GetSection("DataBase")["dataBaseName"];
            string server = "localhost"; //AppName.GetSection("DataBase")["Server"];
            string user = "database"; //AppName.GetSection("DataBase")["User"];
            string password = "Igor6985";  //AppName.GetSection("DataBase")["Password"];

            string connectionString = string.Format("server={0};database={1};uid={2};password={3}",
                server, dataBaseName, user, password);

            var mConn = new MySqlConnection(connectionString);

            try
            {
                mConn.Open();
                try
                {
                    MySqlCommand commandExecution = new MySqlCommand(command, mConn);
                    return commandExecution.ExecuteNonQuery().ToString();
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
