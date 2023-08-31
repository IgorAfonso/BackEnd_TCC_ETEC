using BackEndAplication.Data;
using BackEndAplication.Models;
using System.Globalization;

namespace BackEndAplication.Services
{
    public class ImportBigDataService
    {
        public async Task<Boolean> InsertBigDataOnDataBase(string AuthenticatedUser, string CompleteName, 
            DateOnly BornDate, string CPF, string RG, string Adress, string Number, string Neighborhood, 
            string TeachingInstitution, string HaveBF, string HaveCadUniq,
            string CityTeachingInstitution, string Period)
        {
            var authentiatedUserQuery = string.Format("SELECT * FROM users WHERE users.ID = {0}", AuthenticatedUser);

            return true;
        }
    }
}
