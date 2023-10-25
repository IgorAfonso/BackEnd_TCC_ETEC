using BackEndAplication.Data;
using BackEndAplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_TCC_ETEC.Controllers
{
    [ApiController]
    public class InformationController : Controller
    {
        [HttpGet]
        [Route("/GetUserInformation")]
        [Authorize]
        public object GetUserInformation([FromQuery]string username)
        {
            var query = string.Format("SELECT * FROM users WHERE users.username = '{0}'", username);
            var myConn = new MySQLConnectionWithValue();

            var listInfos = myConn.ConsultUserAllInformation(query);

            return new
            {
                Usuario = listInfos.Result,
            };
        }

        [HttpGet]
        [Route("/GetUserDocuments")]
        [Authorize]
        public object GetUserDocuments([FromQuery] string username, string period)
        {
            var myConn = new MySQLConnectionWithValue();
            var authentiatedUserQuery = string.Format("SELECT ID FROM users WHERE users.username = '{0}'", username);
            var idUser = myConn.ValidateExistingUser(authentiatedUserQuery);

            if (idUser == null)
                return new { message = "Não foi possível encontrar o usuário" };

            var query = string.Format("SELECT * FROM operations WHERE operations.IDUser = '{0}' " +
                "AND operations.Period = '{1}'", idUser.Result, period);
            var listInfos = myConn.ConsultUserDocuments(query);

            return new
            {
                Operation = listInfos.Result,
            };
        }

        [HttpGet]
        [Route("/GetCard")]
        [Authorize]
        public object GetUserCard([FromQuery] string username, string period)
        {
            var myConn = new MySQLConnectionWithValue();
            var authentiatedUserQuery = string.Format("SELECT ID FROM users WHERE users.username = '{0}'", username);
            var idUser = myConn.ValidateExistingUser(authentiatedUserQuery);

            if (idUser == null)
                return new { message = "Não foi possível encontrar o usuário" };

            var query = string.Format("SELECT CompleteName, CPF, Period, TeachingInstitution FROM operations WHERE operations.IDUser = '{0}' " +
                "AND operations.Period = '{1}'", idUser.Result, period);
            var listInfos = myConn.GetUserCard(query);

            return new
            {
                User = listInfos.Result,
                Validade = "30 Dias"
            };
        }

        [HttpGet]
        [Route("/GetAllUsers")]
        public async Task<List<string>> GetAllUsers()
        {
            var query = "SELECT username FROM users";

            var myConn = new MySQLConnectionWithValue();
            var listUser = await myConn.GetAllUsers(query);

            return listUser;
        }
    }
}
