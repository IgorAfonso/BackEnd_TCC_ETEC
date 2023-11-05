using BackEnd_TCC_ETEC.Models;
using BackEndAplication.Models;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using Serilog;

namespace BackEndAplication.Data
{
    public class MySQLConnectionWithValue
    {
        private readonly string _dataBaseSchema = Models.Configuration.GetSectionValue("MySqlServer", "DataBase");
        private readonly string _server = Models.Configuration.GetSectionValue("MySqlServer", "Server");
        private readonly string user = Models.Configuration.GetSectionValue("MySqlServer", "User");
        private readonly string password = Models.Configuration.GetSectionValue("MySqlServer", "Password");
        public async Task<string> ConsultUser(string query)
        {

            string connectionString = string.Format("server={0};database={1};uid={2};pwd={3}",
                _server, _dataBaseSchema, user, password);

            var mConn = new MySqlConnection(connectionString);

            try
            {
                var list = new List<UserLogin>();
                try
                {
                    mConn.Open();
                    Log.Information("[Consulta ao MySql] Executando consulta no banco de dados MySQL.");
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
                    if (string.IsNullOrEmpty(result))
                    {
                        Log.Error(string.Format("[Consulta ao MySql] Falha na consulta do banco de dados.. Nenhum Retorno para a consulta: ({0})", query));
                        return result.ToString();
                    }
                    else
                    {
                        Log.Information("[Consulta ao MySql] Encontrado resultado com sucesso.");
                        return result.ToString();
                    }
                    
                }
                catch (Exception)
                {
                    Log.Error(string.Format("[Consulta ao MySql] Erro ao realizar consulta ({0})", query));
                    return "";
                }
                finally
                {
                    mConn.Close();
                }
            }
            catch (Exception)
            {
                Log.Error(string.Format("[Consulta ao MySql] Erro ao realizar consulta ({0})", query));
                return "";
            }
        }

        public async Task<string> ValidateExistingUser(string query)
        {
            string connectionString = string.Format("server={0};database={1};uid={2};pwd={3}",
                _server, _dataBaseSchema, user, password);

            var mConn = new MySqlConnection(connectionString);

            try
            {
                var list = new List<Users>();
                try
                {
                    mConn.Open();
                    Log.Information("[Consulta ao MySql] Executando consulta no banco de dados MySQL.");
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
                    var lenghtList = list.Count;
                    if (lenghtList == 0)
                    {
                        Log.Error(string.Format("[Consulta ao MySql] Nenhum resultado encontrado na query: ({0})", query));
                        return "userNotFound";
                    }
                    else
                    {
                        Log.Information("[Consulta ao MySql] Encontrado resultado com sucesso.");
                        var result = list[0].Id;
                        return result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("[Consulta ao MySql] Falha ao executar consulta ({0}), erro: {1}", query, ex.Message));
                    return "DatabaseFailure";
                }
                finally
                {
                    mConn.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("[Consulta ao MySql] Falha ao executar consulta ({0}), erro: {1}", query, ex.Message));
                return "DatabaseFailure";
            }
        }

        public async Task<string> ValidatePeriod(string query)
        {
            string connectionString = string.Format("server={0};database={1};uid={2};pwd={3}",
                _server, _dataBaseSchema, user, password);

            var mConn = new MySqlConnection(connectionString);

            try
            {
                var list = new List<bigDataModel>();
                try
                {
                    mConn.Open();
                    Log.Information("[Consulta ao MySql] Executando consulta no banco de dados MySQL.");
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
                        Log.Error(string.Format("[Consulta ao MySql] Nenhum resultado encotrado para a query {0}", query));
                        return "";
                    }
                    else
                    {
                        Log.Information("[Consulta ao MySql] Resultado encontrado com sucesso.");
                        return result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("[Consulta ao MySql] Falha ao executar consulta ({0}), erro: {1}", query, ex.Message));
                    return "";
                }
                finally
                {
                    mConn.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("[Consulta ao MySql] Falha ao executar consulta ({0}), erro: {1}", query, ex.Message));
                return "";
            }
        }

        public async Task<bool> TrueUser(string query, string username, string Password)
        {
            string connectionString = string.Format("server={0};database={1};uid={2};pwd={3}",
                _server, _dataBaseSchema, user, password);

            var mConn = new MySqlConnection(connectionString);


            var list = new List<UserLogin>();
            try
            {
                mConn.Open();
                Log.Information("[Consulta ao MySql] Executando consulta no banco de dados MySQL.");
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
                    Log.Information("[Consulta ao MySql] Feita a validação de usuário e senha com sucesso.");
                    return true;
                }
                else
                {
                    Log.Error("[Consulta ao MySql] Usuário e senha reprovados na verificação com o banco de dados.");
                    return false;
                }

            }
            catch (Exception ex)
            {
                Log.Error(string.Format("[Consulta ao MySql] Falha ao executar consulta ({0}), erro: {1}", query, ex.Message));
                return false;
            }
            finally
            {
                mConn.Close();
            }
        }

        public async Task<List<Users>> ConsultUserAllInformation(string query)
        {
            string connectionString = string.Format("server={0};database={1};uid={2};pwd={3}",
                _server, _dataBaseSchema, user, password);

            var mConn = new MySqlConnection(connectionString);

            try
            {
                var list = new List<Users>();
                try
                {
                    mConn.Open();
                    Log.Information("[Consulta ao MySql] Executando consulta no banco de dados MySQL.");
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
                    if (string.IsNullOrEmpty(list.ToString()))
                    {
                        Log.Error(string.Format("[Consulta ao MySql] Nenhum resultado encontrado para a Consulta {0}", query));
                        return list;
                    }
                    else
                    {
                        Log.Information("[Consulta ao MySql] Consulta Realizada com sucesso.");
                        return list;
                    }
                }
                catch (Exception ex)
                {
                    list.Clear();
                    Users userModel;
                    userModel = new Users();
                    userModel.Id = 0;
                    userModel.Username = "";
                    userModel.Password = "";
                    userModel.email = "";

                    list.Add(userModel);

                    Log.Error(string.Format("[Consulta ao MySql] Falha ao executar consulta ({0}), erro: {1}", query, ex.Message));
                    return list;
                }
                finally
                {
                    mConn.Close();
                }
            }
            catch (Exception ex)
            {
                var list = new List<Users>();
                Users userModel;
                userModel = new Users();
                userModel.Id = 0;
                userModel.Username = "";
                userModel.Password = "";
                userModel.email = "";

                list.Add(userModel);

                Log.Error(string.Format("[Consulta ao MySql] Falha ao executar consulta ({0}), erro: {1}", query, ex.Message));
                return list;
            }
        }

        public async Task<List<BigDataModel>> ConsultUserDocuments(string query)
        {
            string connectionString = string.Format("server={0};database={1};uid={2};pwd={3}",
                _server, _dataBaseSchema, user, password);

            var mConn = new MySqlConnection(connectionString);

            try
            {
                var list = new List<BigDataModel>();
                try
                {
                    mConn.Open();
                    Log.Information("[Consulta ao MySql] Executando consulta no banco de dados MySQL.");
                    await using (MySqlCommand commandExecution = new MySqlCommand(query, mConn))
                    {
                        using (MySqlDataReader reader = commandExecution.ExecuteReader())
                        {
                            BigDataModel userModel;
                            while (reader.Read())
                            {
                                userModel = new BigDataModel();
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
                    if(list.Count > 0)
                    {
                        Log.Information("[Consulta ao MySql] Sucesso ao Realizar consulta ao MySql.");
                        return list;
                    }
                    else
                    {
                        Log.Error(string.Format("[Consulta ao MySql] Falha ao realizar consulta para a query ({0})", query));
                        return list;
                    }
                    
                }
                catch (Exception ex)
                {
                    list.Clear();
                    BigDataModel userModel;
                    userModel = new BigDataModel();
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

                    Log.Error(string.Format("[Consulta ao MySql] Falha ao executar consulta ({0}), erro: {1}", query, ex.Message));
                    return list;
                }
                finally
                {
                    mConn.Close();
                }
            }
            catch (Exception ex)
            {
                var list = new List<BigDataModel>();
                BigDataModel userModel;
                userModel = new BigDataModel();
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

                Log.Error(string.Format("[Consulta ao MySql] Falha ao executar consulta ({0}), erro: {1}", query, ex.Message));
                return list;
            }
        }

        public async Task<List<CardModel>> GetUserCard(string query)
        {
            string connectionString = string.Format("server={0};database={1};uid={2};pwd={3}",
                _server, _dataBaseSchema, user, password);

            var mConn = new MySqlConnection(connectionString);

            try
            {
                var list = new List<CardModel>();
                try
                {
                    mConn.Open();
                    Log.Information("[Consulta ao MySql] Executando consulta no banco de dados MySQL.");
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
                    if (list.Count > 0)
                    {
                        Log.Information("[Consulta ao MySql] Sucesso ao Realizar consulta ao MySql.");
                        return list;
                    }
                    else
                    {
                        Log.Error(string.Format("[Consulta ao MySql] Falha ao retornar resultado na consulta ({0})", query));
                        return list;
                    }
                }
                catch (Exception ex)
                {
                    list.Clear();
                    CardModel cardModel;
                    cardModel = new CardModel();
                    cardModel.CompleteName = "";
                    cardModel.CPF = "";
                    cardModel.Period = "";
                    cardModel.Institution = "";

                    list.Add(cardModel);

                    Log.Error(string.Format("[Consulta ao MySql] Falha ao executar consulta ({0}), erro: {1}", query, ex.Message));
                    return list;
                }
                finally
                {
                    mConn.Close();
                }
            }
            catch (Exception ex)
            {
                var list = new List<CardModel>();
                CardModel cardModel;
                cardModel = new CardModel();
                cardModel.CompleteName = "";
                cardModel.CPF = "";
                cardModel.Period = "";
                cardModel.Institution = "";

                list.Add(cardModel);

                Log.Error(string.Format("[Consulta ao MySql] Falha ao executar consulta ({0}), erro: {1}", query, ex.Message));
                return list;
            }
        }

        public async Task<List<UserVerificarionModelReturnUsers>> GetAllUsers(string query)
        {
            string connectionString = string.Format("server={0};database={1};uid={2};pwd={3}",
                _server, _dataBaseSchema, user, password);

            var mConn = new MySqlConnection(connectionString);

            try
            {
                var list = new List<UserVerificarionModelReturnUsers>();
                try
                {
                    mConn.Open();
                    Log.Information("[Consulta ao MySql] Executando consulta no banco de dados MySQL.");
                    await using (MySqlCommand commandExecution = new MySqlCommand(query, mConn))
                    {
                        using (MySqlDataReader reader = commandExecution.ExecuteReader())
                        {
                            
                        
                            while (reader.Read())
                            {
                                var userModel = new UserVerificarionModelReturnUsers();
                                userModel.UserName = reader[0].ToString();
                                userModel.Email = reader[1].ToString();
                                list.Add(userModel);
                            }
                        }
                    }
                    if (list.Count > 0)
                    {
                        Log.Information("[Consulta ao MySql] Sucesso na consulta ao MySql");
                        return list;
                    }
                    else
                    {
                        Log.Error(string.Format("[Consulta ao MySql] Falha ao retornar resultado para a consulta {0}", query));
                        return list;
                    }
                    
                }
                catch (Exception ex)
                {                    
                    list.Clear();
                    var userModel = new UserVerificarionModelReturnUsers();
                    userModel.UserName = "";
                    list.Add(userModel);

                    Log.Error(string.Format("[Consulta ao MySql] Falha ao executar consulta ({0}), erro: {1}", query, ex.Message));
                    return list;
                }
                finally
                {
                    mConn.Close();
                }
            }
            catch (Exception ex)
            {
                var list = new List<UserVerificarionModelReturnUsers>();              
                var userModel = new UserVerificarionModelReturnUsers();
                userModel.UserName = "";
                list.Add(userModel);

                Log.Error(string.Format("[Consulta ao MySql] Falha ao executar consulta ({0}), erro: {1}", query, ex.Message));
                return list;
            }
        }
    }
}