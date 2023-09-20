using BackEndAplication.Models;
using MySql.Data.MySqlClient;

namespace BackEndAplication.Data
{
    public class MySQLConnectionWithValue
    {
        private string _dataBaseSchema;
        private string _server;
        private string user;
        private string password;
        public async Task<string> ConsultUser(string query)
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
                var list = new List<Users>();
                try
                {
                    mConn.Open();
                    await using (MySqlCommand commandExecution = new MySqlCommand(query, mConn))
                    {
                        using (MySqlDataReader reader = commandExecution.ExecuteReader())
                        {
                            Users userModel;
                            while (reader.Read())
                            {
                                userModel = new Users();
                                userModel.Id = int.Parse(reader[0].ToString());
                                userModel.Username = reader[1].ToString();
                                userModel.Password = reader[2].ToString();
                                userModel.email = reader[3].ToString();

                                list.Add(userModel);
                            }
                        }
                    }
                    var result = list[0].Username;
                    return result.ToString();
                }
                catch (Exception)
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public async Task<string> ValidarExistingUser(string query)
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
                var list = new List<Users>();
                try
                {
                    mConn.Open();
                    await using (MySqlCommand commandExecution = new MySqlCommand(query, mConn))
                    {
                        using (MySqlDataReader reader = commandExecution.ExecuteReader())
                        {
                            Users userModel;
                            while (reader.Read())
                            {
                                userModel = new Users();
                                userModel.Id = int.Parse(reader[0].ToString());

                                list.Add(userModel);
                            }
                        }
                    }
                    var result = list[0].Id;
                    if(result == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return result.ToString();
                    }
                }
                catch (Exception)
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }


    }
}
