using Microsoft.AspNetCore.Hosting.Server;
using MySql.Data.MySqlClient;
using System.Data;

namespace BackEndAplication.Data
{
    public class MySQLConnection
    {
        public string connectionDataBase(string command)
        {
            var mConn = new MySqlConnection(" Persist Security Info=False;" +
                "server=localhost;database=Cadastro;uid=root");

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
        }
    }
}
