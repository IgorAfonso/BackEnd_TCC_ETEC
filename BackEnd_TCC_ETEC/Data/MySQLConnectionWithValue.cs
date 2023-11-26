using BackEnd_TCC_ETEC.Models;
using BackEnd_TCC_ETEC.Services;
using BackEndAplication.Models;
using MySql.Data.MySqlClient;
using Serilog;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

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
                mConn.Open();
                Log.Information("[Consulta ao MySql] Executando consulta no banco de dados MySQL.");
                await
                using (MySqlCommand commandExecution = new MySqlCommand(query, mConn))
                {
                    using MySqlDataReader reader = commandExecution.ExecuteReader();
                    UserLogin userModel;
                    while (reader.Read())
                    {
                        userModel = new UserLogin();
                        userModel.UserName = reader[0].ToString() ?? string.Empty;
                        userModel.Password = reader[1].ToString() ?? string.Empty;

                        list.Add(userModel);
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

        public async Task<string> ValidateExistingUser(string query)
        {
            string connectionString = string.Format("server={0};database={1};uid={2};pwd={3}",
                _server, _dataBaseSchema, user, password);

            var mConn = new MySqlConnection(connectionString);

            try
            {
                mConn.Open();
                var list = new List<CreateUserId>();
                Log.Information("[Consulta ao MySql] Executando consulta no banco de dados MySQL.");
                await using (MySqlCommand commandExecution = new MySqlCommand(query, mConn))
                {
                    using MySqlDataReader reader = commandExecution.ExecuteReader();
                    CreateUserId userModel;
                    while (reader.Read())
                    {
                        userModel = new CreateUserId();
                        userModel.Id = int.Parse(reader[0].ToString() ?? string.Empty);

                        list.Add(userModel);
                    }
                }
                var lenghtList = list[0].Id;
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

        public async Task<string> ValidatePeriod(string query)
        {
            string connectionString = string.Format("server={0};database={1};uid={2};pwd={3}",
                _server, _dataBaseSchema, user, password);

            var mConn = new MySqlConnection(connectionString);

            try
            {
                mConn.Open();
                var list = new List<bigDataModel>();
                Log.Information("[Consulta ao MySql] Executando consulta no banco de dados MySQL.");
                await using (MySqlCommand commandExecution = new MySqlCommand(query, mConn))
                {
                    using (MySqlDataReader reader = commandExecution.ExecuteReader())
                    {
                        bigDataModel DataModel;
                        while (reader.Read())
                        {
                            DataModel = new bigDataModel();
                            DataModel.Period = reader[0].ToString() ?? string.Empty;

                            list.Add(DataModel);
                        }
                    }
                }
                var result = list[0].Period;
                if (string.IsNullOrEmpty(result))
                {
                    Log.Error(string.Format("[Consulta ao MySql] Nenhum resultado encotrado para a query {0}", query));
                    return string.Empty;
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

        public async Task<bool> TrueUser(string query, string username, string Password)
        {
            string connectionString = string.Format("server={0};database={1};uid={2};pwd={3}",
                _server, _dataBaseSchema, user, password);

            var mConn = new MySqlConnection(connectionString);

            try
            {
                mConn.Open();
                var list = new List<UserLogin>();
                Log.Information("[Consulta ao MySql] Executando consulta no banco de dados MySQL.");
                await using (MySqlCommand commandExecution = new MySqlCommand(query, mConn))
                {
                    using (MySqlDataReader reader = commandExecution.ExecuteReader())
                    {
                        UserLogin userModel;
                        while (reader.Read())
                        {
                            userModel = new UserLogin();
                            userModel.UserName = reader[0].ToString() ?? string.Empty;
                            userModel.Password = reader[1].ToString() ?? string.Empty;

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
                            userModel.Id = int.Parse(reader[0].ToString() ?? "0");
                            userModel.Username = reader[1].ToString() ?? string.Empty;
                            userModel.Password = "*****";
                            userModel.email = reader[3].ToString() ?? string.Empty;

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
                list.Add(new Users());
                Log.Error(string.Format("[Consulta ao MySql] Falha ao executar consulta ({0}), erro: {1}", query, ex.Message));
                return list;
            }
            finally
            {
                mConn.Close();
            }
        }

        public async Task<List<BigDataModel>> ConsultUserDocuments(string query)
        {
            string connectionString = string.Format("server={0};database={1};uid={2};pwd={3}",
                _server, _dataBaseSchema, user, password);

            var mConn = new MySqlConnection(connectionString);
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
                            userModel.user = reader[0].ToString() ?? string.Empty;
                            userModel.CompleteName = reader[1].ToString() ?? string.Empty;
                            userModel.OperationDate = reader[2].ToString() ?? string.Empty;
                            userModel.BornDate = reader[3].ToString() ?? string.Empty;
                            userModel.CPF = reader[4].ToString() ?? string.Empty;
                            userModel.RG = reader[5].ToString() ?? string.Empty;
                            userModel.Adress = reader[6].ToString() ?? string.Empty;
                            userModel.Number = reader[7].ToString() ?? string.Empty;
                            userModel.Neighborhood = reader[8].ToString() ?? string.Empty;
                            userModel.TeachingInstitution = reader[9].ToString() ?? string.Empty;
                            userModel.HaveBF = reader[10].ToString() ?? string.Empty;
                            userModel.HaveCadUniq = reader[11].ToString() ?? string.Empty;
                            userModel.CityTeachingInstitution = reader[12].ToString() ?? string.Empty;
                            userModel.Period = reader[13].ToString() ?? string.Empty;
                            userModel.TermsOfUse = reader[14].ToString() ?? string.Empty;
                            userModel.MonthStudy = reader[15].ToString() ?? string.Empty;

                            list.Add(userModel);
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
                    Log.Error(string.Format("[Consulta ao MySql] Falha ao realizar consulta para a query ({0})", query));
                    return list;
                }

            }
            catch (Exception ex)
            {
                Log.Error(string.Format("[Consulta ao MySql] Falha ao executar consulta ({0}), erro: {1}", query, ex.Message));
                list.Clear();
                list.Add(new BigDataModel());
                return list;
            }
            finally
            {
                mConn.Close();
            }
        }

        public async Task<List<CardModel>> GetUserCard(string query)
        {
            string connectionString = string.Format("server={0};database={1};uid={2};pwd={3}",
                _server, _dataBaseSchema, user, password);

            var mConn = new MySqlConnection(connectionString);

            var list = new List<CardModel>();
            try
            {
                mConn.Open();
                Log.Information("[Consulta ao MySql] Executando consulta no banco de dados MySQL.");
                var encrypt = new EncryptService();
                await using (MySqlCommand commandExecution = new MySqlCommand(query, mConn))
                {
                    using (MySqlDataReader reader = commandExecution.ExecuteReader())
                    {
                        CardModel cardModel;
                        while (reader.Read())
                        {
                            cardModel = new CardModel();
                            cardModel.CompleteName = reader[0].ToString() ?? string.Empty;
                            cardModel.CPF = reader[1].ToString() ?? string.Empty;
                            cardModel.Institution = reader[2].ToString() ?? string.Empty;
                            cardModel.Period = reader[3].ToString() ?? string.Empty;
                            cardModel.FinalValidDate = string.IsNullOrEmpty(reader[4].ToString()) ? string.Empty : DateTime.Parse(reader[4].ToString()).ToString("dd/MM/yyyy");
                            cardModel.ColorMonth = reader[6].ToString() ?? string.Empty;
                            cardModel.CardImage = encrypt.ObjToByte(reader[5]);

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
                    list.Clear();
                    list.Add(new CardModel());
                    return list;
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("[Consulta ao MySql] Falha ao executar consulta ({0}), erro: {1}", query, ex.Message));
                list.Clear();
                list.Add(new CardModel());
                return list;
            }
            finally
            {
                mConn.Close();
            }
        }

        public async Task<List<UserVerificarionModelReturnUsers>> GetAllUsers(string query)
        {
            string connectionString = string.Format("server={0};database={1};uid={2};pwd={3}",
                _server, _dataBaseSchema, user, password);

            var mConn = new MySqlConnection(connectionString);

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
                            userModel.UserName = reader[0].ToString() ?? string.Empty;
                            userModel.Email = reader[1].ToString() ?? string.Empty;
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

        public async Task<DateTime> GetSimpleInformation(string query)
        {
            string connectionString = string.Format("server={0};database={1};uid={2};pwd={3}",
                _server, _dataBaseSchema, user, password);

            var mConn = new MySqlConnection(connectionString);

            var result = "";

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
                            result = reader.GetString(0);
                        }
                    }
                }

                if (string.IsNullOrEmpty(result))
                {
                    result = "2049-12-31";
                    Log.Error(string.Format("[Consulta ao MySql] Não foi encontrado resultado para a query ({0})", query));
                    return Convert.ToDateTime(result);
                }
                else
                {
                    Log.Information("[Consulta ao MySql] Consulta executada com sucesso.");
                    return Convert.ToDateTime(result);
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("[Consulta ao MySql] Falha ao Executar Consulta. Erro: {0}", ex.Message));
                return Convert.ToDateTime(result);
            }
            finally 
            { 
                mConn.Close();
            }
        }

        

        public async Task<String> GetImageString(string query)
        {
            string connectionString = string.Format("server={0};database={1};uid={2};pwd={3}",
                _server, _dataBaseSchema, user, password);

            var mConn = new MySqlConnection(connectionString);

            var result = "";

            mConn.Open();
            Log.Information("[Consulta ao MySql] Executando consulta no banco de dados MySQL.");
            await using (MySqlCommand commandExecution = new MySqlCommand(query, mConn))
            {
                using (MySqlDataReader reader = commandExecution.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = reader.GetString(0);
                    }
                }
            }

            if (string.IsNullOrEmpty(result))
            {
                return string.Empty;
            }

            return result;
        }
    }
}