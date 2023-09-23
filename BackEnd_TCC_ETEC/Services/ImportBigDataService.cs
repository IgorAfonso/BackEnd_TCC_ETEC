using BackEndAplication.Data;
using BackEndAplication.Models;
using System.Globalization;

namespace BackEndAplication.Services
{
    public class ImportBigDataService
    {
        public async Task<Boolean> InsertBigDataOnDataBase(string AuthenticatedUser, string CompleteName, 
            string BornDate, string CPF, string RG, string Adress, string Number, string Neighborhood, 
            string TeachingInstitution, string HaveBF, string HaveCadUniq,
            string CityTeachingInstitution, string Period)
        {
            var dataBaseCon = new MySQLConnectionWithValue();

            var authentiatedUserQuery = string.Format("SELECT ID FROM users WHERE users.username = '{0}'", AuthenticatedUser);
            var idUser = dataBaseCon.ValidateExistingUser(authentiatedUserQuery);

            if (string.IsNullOrEmpty(idUser.Result))
                return false;

            var validationPeriod = string.Format("SELECT Period FROM operations WHERE operations.IDUser = {0} ORDER BY operations.OperationDate DESC", idUser.Result);
            var periodUser = dataBaseCon.ValidatePeriod(validationPeriod);

            if(periodUser.Result == Period)
                return false;

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

                if (resultOperation == "0") return false;
                if (resultAdress == "0") return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
