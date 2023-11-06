﻿using BackEndAplication.Data;
using BackEndAplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Globalization;

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

            var queryLastPeriod = string.Format("select\r\n\t" +
                "cast(str_to_date(REPLACE(query.Period, '-', '.'),'%d.%m.%Y') as DATE)finalDate" +
                " \r\nfrom(\r\n\tselect \r\n\tconcat('01-',operations.Period) Period \r\n\t" +
                "from operations\r\n\tinner join users on users.ID = operations.IDUser\r\n\t" +
                "where users.username = '{0}'\r\n) query\r\norder by finalDate desc\r\n", username);

            var lastPeriodunFormated = myConn.GetSimpleInformation(queryLastPeriod).Result;
            var lastPeriod = lastPeriodunFormated.ToString("MM-yyyy");

            Log.Information("[HttpGet] GetUserInformation realizado para o usuário {0}", username);
            return new
            {
                Usuario = listInfos.Result,
                lastPeriod,
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
            {
                Log.Error(string.Format("Usuário não encontrado nos registros: {0}", username));
                return new { message = "Não foi possível encontrar o usuário" };
            }
                

            var query = string.Format("SELECT * FROM operations WHERE operations.IDUser = '{0}' " +
                "AND operations.Period = '{1}'", idUser.Result, period);
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
        public object GetUserCard([FromQuery] string username, string period)
        {
            var myConn = new MySQLConnectionWithValue();
            var authentiatedUserQuery = string.Format("SELECT ID FROM users WHERE users.username = '{0}'", username);
            var idUser = myConn.ValidateExistingUser(authentiatedUserQuery);

            if (idUser == null)
            {
                Log.Error(string.Format("[HttpGet] Erro ao encontrar o usuário {0}", username));
                return new { message = "Não foi possível encontrar o usuário" };
            }
                

            var query = string.Format("SELECT CompleteName, CPF, Period, TeachingInstitution FROM operations WHERE operations.IDUser = '{0}' " +
                "AND operations.Period = '{1}'", idUser.Result, period);
            var listInfos = myConn.GetUserCard(query);

            Log.Information(string.Format("[HttpGet] GetUserCard Consulta Realizada para o usuário {0}", username));
            return new
            {
                User = listInfos.Result,
                Validade = "30 Dias"
            };
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
