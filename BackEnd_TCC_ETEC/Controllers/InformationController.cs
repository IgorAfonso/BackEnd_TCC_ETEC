using BackEnd_TCC_ETEC.Models;
using BackEnd_TCC_ETEC.Services;
using BackEndAplication.Data;
using BackEndAplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Globalization;
using System.Text.Json;

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

            var queryLastPeriod = string.Format("select\r\nIFNULL(\r\nMAX(\r\ncast(\r\nstr_to_date(\r\nREPLACE(query.Period, '-', '.')\r\n,'%d.%m.%Y')\r\nas DATE)), \"2049-12-31\")\r\nfinalDate\r\nfrom(select concat('01-',operations.MonthStudy) Period\r\nfrom operations inner join users on users.ID = operations.IDUser\r\nwhere users.username = '{0}') query order by finalDate desc", username);

            var lastPeriodunFormated = myConn.GetSimpleInformation(queryLastPeriod).Result;
            var lastPeriod = lastPeriodunFormated.ToString("MM-yyyy");

            if (lastPeriod == "12-2049")
                return new UserGetInformations
                {
                    Id = 0,
                    Username = "",
                    Email = "",
                    LastPeriod = "",
                };

            Log.Information("[HttpGet] GetUserInformation realizado para o usuário {0}", username);
            return new UserGetInformations
            {
                Id = listInfos.Result[0].Id,
                Username = listInfos.Result[0].Username,
                Email = listInfos.Result[0].email,
                LastPeriod = lastPeriod,
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

            if (idUser.Result == null)
            {
                Log.Error(string.Format("Usuário não encontrado nos registros: {0}", username));
                return BadRequest(new { message = "Não foi possível encontrar o usuário" });
            }

            var query = string.Format("SELECT operations.IDUser, operations.CompleteName, operations.OperationDate, operations.BornDate, operations.CPF, operations.RG, adress.Adress, adress.`Number`, adress.Neightborhood, operations.TeachingInstitution, operations.HaveBF, operations.HaveCadUniq, operations.CityTeachingInstitutin, operations.Period, operations.TermsOfUse, operations.MonthStudy FROM operations inner join adress on adress.IDUser = operations.IDUser WHERE operations.IDUser = '{0}' AND operations.MonthStudy = '{1}'", idUser.Result, period);
            var listInfos = myConn.ConsultUserDocuments(query);

            Log.Information(string.Format("[HttpGet] GetUserDocuments realizado para o usuário: {0}", username));
            return new
            {
                Operation = listInfos.Result,
            };
        }

        [HttpGet]
        [Route("/GetCard")]
        [Authorize]
        public object GetUserCard([FromQuery] string username, string MonthStudy)
        {
            var myConn = new MySQLConnectionWithValue();
            var authentiatedUserQuery = string.Format("SELECT ID FROM users WHERE users.username = '{0}'", username);
            var idUser = myConn.ValidateExistingUser(authentiatedUserQuery);

            if (idUser == null)
            {
                Log.Error(string.Format("[HttpGet] Erro ao encontrar o usuário {0}", username));
                return new { message = "Não foi possível encontrar o usuário" };
            }

            var query = string.Format("select\r\nfinalQuery.*,\r\ncolors.ColorOfMonth\r\nfrom \r\n(select \r\nquery.CompleteName,\r\nquery.CPF,\r\nquery.TeachingInstitution,\r\nquery.Period,\r\nLAST_DAY(cast(str_to_date(REPLACE\r\n(query.MonthStudy, '-', '.'),'%d.%m.%Y') as DATE))finalDate,\r\nquery.CardImage\r\nfrom (\r\nselect\r\noperations.CompleteName, operations.CPF, operations.TeachingInstitution,operations.Period,\r\nconcat('01-',operations.MonthStudy) MonthStudy, users.CardImage from operations\r\ninner join users on users.ID = operations.IDUser where users.username = '{0}'\r\nAND operations.MonthStudy = '{1}') query\r\norder by finalDate desc) finalQuery\r\ninner join colors on colors.FirstDay = month(finalQuery.finalDate)",
                username, MonthStudy);
            var listInfos = myConn.GetUserCard(query);

            Log.Information(string.Format("[HttpGet] GetUserCard Consulta Realizada para o usuário {0}", username));
            var final = new CardModel
            {
                CompleteName = listInfos.Result[0].CompleteName,
                CPF = listInfos.Result[0].CPF,
                Period = listInfos.Result[0].Period,
                Institution = listInfos.Result[0].Institution,
                FinalValidDate = listInfos.Result[0].FinalValidDate,
                ColorMonth = listInfos.Result[0].ColorMonth,
                CardImage = listInfos.Result[0].CardImage != "" ? listInfos.Result[0].CardImage : ""
            };

            return final;
        }

        [HttpGet]
        [Route("/GetAllUsers")]
        public async Task<object> GetAllUsers()
        {
            var query = "SELECT username, email FROM users";

            var myConn = new MySQLConnectionWithValue();
            var listUser =  myConn.GetAllUsers(query);

            Log.Information("[HttpGet] GetAllUsers executado com sucesso.");
            return new
            {
                User = listUser.Result,
            };
        }
    }
}
