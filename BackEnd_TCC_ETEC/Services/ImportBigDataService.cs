using BackEndAplication.Data;
using BackEndAplication.Models;
using System.Globalization;
using Serilog;

namespace BackEndAplication.Services
{
    public class ImportBigDataService
    {
        public async Task<String> InsertBigDataOnDataBase(string AuthenticatedUser, string CompleteName, 
            string BornDate, string CPF, string RG, string Adress, string Number, string Neighborhood, 
            string TeachingInstitution, string HaveBF, string HaveCadUniq,
            string CityTeachingInstitution, string Period)
        {
            var dataBaseCon = new MySQLConnectionWithValue();

            var authentiatedUserQuery = string.Format("SELECT ID FROM users WHERE users.username = '{0}'", AuthenticatedUser);
            var idUser = dataBaseCon.ValidateExistingUser(authentiatedUserQuery);

            if (idUser.Result == "userNotFound")
                return "UserNotFound";

            var validationPeriod = string.Format("SELECT Period FROM operations WHERE operations.IDUser = {0} ORDER BY operations.OperationDate DESC", idUser.Result);
            var periodUser = dataBaseCon.ValidatePeriod(validationPeriod);

            if(periodUser.Result == Period)
                return "PeriodExists";

            var insertAuthenticatedUserOperations = string.Format("INSERT INTO operations (IDUser, CompleteName, " +
                "OperationDate, BornDate, CPF, RG, TeachingInstitution,HaveBF, HaveCadUniq, CityTeachingInstitutin, " +
                "Period) VALUES ({0}, '{1}', '{2}', '{3}', '{4}','{5}', '{6}', '{7}', '{8}', '{9}', '{10}')", idUser.Result, CompleteName,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), BornDate, CPF, RG, TeachingInstitution, HaveBF, HaveCadUniq, CityTeachingInstitution, Period);

            var insertAuthenticatedUserAdress = string.Format("INSERT INTO adress (IDUser, Adress, Number, Neightborhood, Period)" +
                " VALUES ({0}, '{1}', '{2}', '{3}', '{4}')", idUser.Result, Adress, Number, Neighborhood, Period);

            try
            {
                var dataInsert = new MySQLConnection();
                var resultOperation = dataInsert.connectionDataBase(insertAuthenticatedUserOperations).ToString();
                var resultAdress = dataInsert.connectionDataBase(insertAuthenticatedUserAdress).ToString();

                if (resultOperation == "DatabaseFailure") return "DatabaseFailure";
                if (resultAdress == "DatabaseFailure") return "DatabaseFailure";

                return "Sucssess";
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Serviço de importação falhou pelo seguinte motivo: {0}", ex.Message));
                return ex.Message;
            }
        }
    }
}
