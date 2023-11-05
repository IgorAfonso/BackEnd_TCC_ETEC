using BackEndAplication.Models;
using BackEndAplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BackEndAplication.Controllers
{
    [ApiController]
    public class BigDataSetController : ControllerBase
    {
        [HttpPost]
        [Route("importData")]
        [Authorize]
        public async Task<ActionResult<dynamic>> ImportDataSet([FromBody] bigDataModel model)
        {
            var insertService = new ImportBigDataService();

            if (model.user.Length > 50) return BadRequest("Campo de usuário não pode ter mais que 50 caracteres");
            if (model.CompleteName.Length > 100) return BadRequest("Campo de CompleteName não pode ter mais que 100 caracteres");
            if (model.BornDate.Length > 10) return BadRequest("Campo de BornDate não pode ter mais que 10 caracteres");
            if (model.CPF.Length > 15) return BadRequest("Campo de CPF não pode ter mais que 15 caracteres");
            if (model.RG.Length > 20) return BadRequest("Campo de RG não pode ter mais que 20 caracteres");
            if (model.Adress.Length > 100) return BadRequest("Campo de Adress não pode ter mais que 100 caracteres");
            if (model.Number.Length > 10) return BadRequest("Campo de Number não pode ter mais que 10 caracteres");
            if (model.Neighborhood.Length > 100) return BadRequest("Campo de Neighborhood não pode ter mais que 100 caracteres");
            if (model.TeachingInstitution.Length > 100) return BadRequest("Campo de TeachingInstitution não pode ter mais que 100 caracteres");
            if (model.HaveBF.Length > 1) return BadRequest("Campo de HaveBF não pode ter mais que 1 caracter");
            if (model.HaveCadUniq.Length > 1) return BadRequest("Campo de HaveCadUniq não pode ter mais que 1 caracter");
            if (model.CityTeachingInstitution.Length > 100) return BadRequest("Campo de CityTeachingInstitution não pode ter mais que 100 caracteres");
            if (model.Period.Length > 50) return BadRequest("Campo de Period não pode ter mais que 50 caracteres");

            var response = await insertService.InsertBigDataOnDataBase(model.user, model.CompleteName, model.BornDate,
                model.CPF, model.RG, model.Adress, model.Number, model.Neighborhood, model.TeachingInstitution,
                model.HaveBF, model.HaveCadUniq, model.CityTeachingInstitution, model.Period);

            string failResponse = "Falha na inserção dos Dados.";
            string successResponse = "Sucesso na Inserção!";

            if (response == "UserNotFound")
            {
                Log.Error(string.Format("[HttpPost] Usuário não encontrado ({0}) com uma conta criada.", model.user));
                return BadRequest(string.Format("{0} Usuário não encontrado ({1}) com uma conta criada.", failResponse, model.user));
            }
            else if(response == "PeriodExists")
            {
                Log.Error(string.Format("[HttpPost] Falha na inserção de documentos para o usuário: {0} no mês {1} (Mês já existe para este usuário)",
                    model.user, model.Period));
                return BadRequest(string.Format("{0} mês com documentos já existentes para este usuário.", failResponse));
            }
            else if(response == "DatabaseFailure")
            {
                Log.Error(string.Format("[HttpPost] Falha com o banco de dados {0} para o usuário {1}", response, model.user));
                return "Falha ao inserir dados no banco de dados.";
            }
            else if(response == "Sucssess")
            {
                Log.Information("Sucesso na inserção");
                return Ok(successResponse);
            }
            else
            {
                Log.Error(string.Format("Falha na inserção. Exception {0}", response));
                return BadRequest(string.Format("Falha na inserção de dados no banco. Exception: {0}", response));
            }
        }
    }
}