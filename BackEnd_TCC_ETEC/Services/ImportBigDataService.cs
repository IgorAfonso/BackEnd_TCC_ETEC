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
            string CityTeachingInstitution, string Period, string TermsOfUse, string MonthStudy)
        {
            var dataBaseCon = new MySQLConnectionWithValue();

            var authentiatedUserQuery = string.Format("select \r\n\tcase when COUNT(ID) = 0 then 0\r\n\twhen COUNT(ID) > 0 then (select ID from users where username = '{0}')\r\nend 'ID'\r\nfrom users where username  = '{0}'\r\n", AuthenticatedUser);
            var idUser = dataBaseCon.ValidateExistingUser(authentiatedUserQuery);

            if (idUser.Result == "userNotFound")
                return "UserNotFound";

            var validationPeriod = string.Format("SELECT MonthStudy FROM operations WHERE operations.IDUser = {0} ORDER BY operations.OperationDate DESC", idUser.Result);
            var periodUser = dataBaseCon.ValidatePeriod(validationPeriod);

            if(periodUser.Result == MonthStudy)
                return "PeriodExists";

            var insertAuthenticatedUserOperations = string.Format("INSERT INTO operations (IDUser, CompleteName, " +
                "OperationDate, BornDate, CPF, RG, TeachingInstitution,HaveBF, HaveCadUniq, CityTeachingInstitutin, " +
                "Period, TermsOfUse, MonthStudy) VALUES ({0}, '{1}', '{2}', '{3}', '{4}','{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}')", idUser.Result, CompleteName,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), BornDate, CPF, RG, TeachingInstitution, HaveBF, HaveCadUniq, CityTeachingInstitution, Period, TermsOfUse, MonthStudy);

            var insertAuthenticatedUserAdress = string.Format("INSERT INTO adress (IDUser, Adress, Number, Neightborhood, MonthStudy)" +
                " VALUES ({0}, '{1}', '{2}', '{3}', '{4}')", idUser.Result, Adress, Number, Neighborhood, MonthStudy);

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

        public async Task<String> UpdateBigDataOnDataBase(string AuthenticatedUser, string CompleteName,
            string BornDate, string CPF, string RG, string Adress, string Number, string Neighborhood,
            string TeachingInstitution, string HaveBF, string HaveCadUniq,
            string CityTeachingInstitution, string Period, string TermsOfUse, string MonthStudy)
        {
            var dataBaseCon = new MySQLConnectionWithValue();

            var authentiatedUserQuery = string.Format("select \r\n\tcase when COUNT(ID) = 0 then 0\r\n\twhen COUNT(ID) > 0 then (select ID from users where username = '{0}')\r\nend 'ID'\r\nfrom users where username  = '{0}'\r\n", AuthenticatedUser);
            var idUser = dataBaseCon.ValidateExistingUser(authentiatedUserQuery);

            if (idUser.Result == "userNotFound")
                return "UserNotFound";

            var insertAuthenticatedUserOperations = string.Format("update operations set TeachingInstitution = '{0}' and HaveBF = '{1}' and HaveCadUniq = '{2}' \r\nand CityTeachingInstitutin = '{3}' and Period = '{4}' and TermsOfUse = '{5}' and MonthStudy = '{6}'\r\nwhere IDUser = '{7}'", idUser.Result, CompleteName,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), BornDate, CPF, RG, TeachingInstitution, HaveBF, HaveCadUniq, CityTeachingInstitution, Period, TermsOfUse, MonthStudy);

            var insertAuthenticatedUserAdress = string.Format("INSERT INTO adress (IDUser, Adress, Number, Neightborhood, MonthStudy)" +
                " VALUES ({0}, '{1}', '{2}', '{3}', '{4}')", idUser.Result, Adress, Number, Neighborhood, MonthStudy);

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
