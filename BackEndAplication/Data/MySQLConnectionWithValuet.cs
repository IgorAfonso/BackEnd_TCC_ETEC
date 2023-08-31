using BackEndAplication.Models;
using MySql.Data.MySqlClient;

namespace BackEndAplication.Data
{
    public class MySQLConnectionWithValuet
    {
        public async Task<dynamic> ConsultUser(string query)
        {
            var AppName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string dataBaseName = AppName.GetSection("DataBase")["dataBaseName"];
            string server = AppName.GetSection("DataBase")["Server"];
            string user = AppName.GetSection("DataBase")["User"];
            string password = AppName.GetSection("DataBase")["Password"];

            string connectionString = string.Format("server={0};database={1};uid={2};password={3}",
                server, dataBaseName, user, password);

            var mConn = new MySqlConnection(connectionString);

            try
            {
                mConn.Open();
                try
                {
                    MySqlCommand commandExecution = new MySqlCommand(query, mConn);
                    List<string> list = new List<string>();
                    list.Add(new Users { };
                    ;
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
        }   
    }
}
