using BackEnd_TCC_ETEC.Models;
using BackEndAplication.Models;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;

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
                var list = new List<UserLogin>();
                try
                {
                    mConn.Open();
                    await using (MySqlCommand commandExecution = new MySqlCommand(query, mConn))
                    {
                        using (MySqlDataReader reader = commandExecution.ExecuteReader())
                        {
                            UserLogin userModel;
                            while (reader.Read())
                            {
                                userModel = new UserLogin();
                                userModel.UserName = reader[0].ToString();
                                userModel.Password = reader[1].ToString();

                                list.Add(userModel);
                            }
                        }
                    }
                    var result = list[0].UserName;
                    return result.ToString();
                }
                catch (Exception)
                {
                    return "";
                }
                finally
                {
                    mConn.Close();
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public async Task<string> ValidateExistingUser(string query)
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
                finally
                {
                    mConn.Close();
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public async Task<string> ValidatePeriod(string query)
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
                var list = new List<bigDataModel>();
                try
                {
                    mConn.Open();
                    await using (MySqlCommand commandExecution = new MySqlCommand(query, mConn))
                    {
                        using (MySqlDataReader reader = commandExecution.ExecuteReader())
                        {
                            bigDataModel DataModel;
                            while (reader.Read())
                            {
                                DataModel = new bigDataModel();
                                DataModel.Period = reader[0].ToString();

                                list.Add(DataModel);
                            }
                        }
                    }
                    var result = list[0].Period;
                    if (string.IsNullOrEmpty(result))
                    {
                        return "";
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
                finally
                {
                    mConn.Close();
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public async Task<bool> TrueUser(string query, string username, string Password)
        {
            _dataBaseSchema = Models.Configuration.GetSectionValue("MySqlServer", "DataBase");
            _server = Models.Configuration.GetSectionValue("MySqlServer", "Server");
            user = Models.Configuration.GetSectionValue("MySqlServer", "User");
            password = Models.Configuration.GetSectionValue("MySqlServer", "Password");

            string connectionString = string.Format("server={0};database={1};uid={2};pwd={3}",
                _server, _dataBaseSchema, user, password);

            var mConn = new MySqlConnection(connectionString);


            var list = new List<UserLogin>();
            try
            {
                mConn.Open();
                await using (MySqlCommand commandExecution = new MySqlCommand(query, mConn))
                {
                    using (MySqlDataReader reader = commandExecution.ExecuteReader())
                    {
                        UserLogin userModel;
                        while (reader.Read())
                        {
                            userModel = new UserLogin();
                            userModel.UserName = reader[0].ToString();
                            userModel.Password = reader[1].ToString();

                            list.Add(userModel);
                        }
                    }
                }
                var user = list[0].UserName;
                var pass = list[0].Password;

                if (user == username && pass == Password)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }
            finally
            {
                mConn.Close();
            }
        }

        public async Task<List<Users>> ConsultUserAllInformation(string query)
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
                                userModel.Password = "*****";
                                userModel.email = reader[3].ToString();

                                list.Add(userModel);
                            }
                        }
                    }
                    return list;
                }
                catch (Exception)
                {
                    list.Clear();
                    Users userModel;
                    userModel = new Users();
                    userModel.Id = 0;
                    userModel.Username = "";
                    userModel.Password = "";
                    userModel.email = "";

                    list.Add(userModel);

                    return list;
                }
                finally
                {
                    mConn.Close();
                }
            }
            catch (Exception)
            {
                var list = new List<Users>();
                Users userModel;
                userModel = new Users();
                userModel.Id = 0;
                userModel.Username = "";
                userModel.Password = "";
                userModel.email = "";

                list.Add(userModel);

                return list;
            }
        }

        public async Task<List<bigDataModel>> ConsultUserDocuments(string query)
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
                var list = new List<bigDataModel>();
                try
                {
                    mConn.Open();
                    await using (MySqlCommand commandExecution = new MySqlCommand(query, mConn))
                    {
                        using (MySqlDataReader reader = commandExecution.ExecuteReader())
                        {
                            bigDataModel userModel;
                            while (reader.Read())
                            {
                                userModel = new bigDataModel();
                                userModel.user = reader[0].ToString();
                                userModel.CompleteName = reader[1].ToString();
                                userModel.OperationDate = reader[2].ToString();
                                userModel.BornDate = reader[3].ToString();
                                userModel.CPF = reader[4].ToString();
                                userModel.RG = reader[5].ToString();
                                userModel.TeachingInstitution = reader[6].ToString();
                                userModel.HaveBF = reader[7].ToString();
                                userModel.HaveCadUniq = reader[8].ToString();
                                userModel.CityTeachingInstitution = reader[9].ToString();
                                userModel.Period = reader[10].ToString();

                                list.Add(userModel);
                            }
                        }
                    }
                    return list;
                }
                catch (Exception)
                {
                    list.Clear();
                    bigDataModel userModel;
                    userModel = new bigDataModel();
                    userModel.user = "";
                    userModel.CompleteName = "";
                    userModel.OperationDate = "";
                    userModel.BornDate = "";
                    userModel.CPF = "";
                    userModel.RG = "";
                    userModel.TeachingInstitution = "";
                    userModel.HaveBF = "";
                    userModel.HaveCadUniq = "";
                    userModel.CityTeachingInstitution = "";
                    userModel.Period = "";

                    list.Add(userModel);

                    return list;
                }
                finally
                {
                    mConn.Close();
                }
            }
            catch (Exception)
            {
                var list = new List<bigDataModel>();
                bigDataModel userModel;
                userModel = new bigDataModel();
                userModel.user = "";
                userModel.CompleteName = "";
                userModel.OperationDate = "";
                userModel.BornDate = "";
                userModel.CPF = "";
                userModel.RG = "";
                userModel.TeachingInstitution = "";
                userModel.HaveBF = "";
                userModel.HaveCadUniq = "";
                userModel.CityTeachingInstitution = "";
                userModel.Period = "";

                list.Add(userModel);

                return list;
            }
        }

        public async Task<List<CardModel>> GetUserCard(string query)
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
                var list = new List<CardModel>();
                try
                {
                    mConn.Open();
                    await using (MySqlCommand commandExecution = new MySqlCommand(query, mConn))
                    {
                        using (MySqlDataReader reader = commandExecution.ExecuteReader())
                        {
                            CardModel cardModel;
                            while (reader.Read())
                            {
                                cardModel = new CardModel();
                                cardModel.CompleteName = reader[0].ToString();
                                cardModel.CPF = reader[1].ToString();
                                cardModel.Period = reader[2].ToString();
                                cardModel.Institution = reader[3].ToString();

                                list.Add(cardModel);
                            }
                        }
                    }
                    return list;
                }
                catch (Exception)
                {
                    list.Clear();
                    CardModel cardModel;
                    cardModel = new CardModel();
                    cardModel.CompleteName = "";
                    cardModel.CPF = "";
                    cardModel.Period = "";
                    cardModel.Institution = "";

                    list.Add(cardModel);

                    return list;
                }
                finally
                {
                    mConn.Close();
                }
            }
            catch (Exception)
            {
                var list = new List<CardModel>();
                CardModel cardModel;
                cardModel = new CardModel();
                cardModel.CompleteName = "";
                cardModel.CPF = "";
                cardModel.Period = "";
                cardModel.Institution = "";

                list.Add(cardModel);

                return list;
            }
        }
    }
}
