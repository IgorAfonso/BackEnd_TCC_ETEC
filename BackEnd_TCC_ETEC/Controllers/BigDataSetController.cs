using BackEnd_TCC_ETEC.Models;
using BackEndAplication.Data;
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
        public async Task<ActionResult<dynamic>> ImportDataSet([FromQuery] UserVerificationModel userModel, [FromBody] bigDataModel model)
        {
            if (userModel.UserName.Length > 50) return BadRequest("Campo de usuário não pode ter mais que 50 caracteres");
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
            if (model.TermsOfUse.Length > 1) return BadRequest("O campo termos de uso não pode conter mais que 1 caracter (Y, N)");
            if (model.MonthStudy.Length > 50) return BadRequest("O campo MonthStudy não pode conter mais que 50 caracteres");

            if (model.TermsOfUse != "Y" && model.TermsOfUse != ("N"))
                return BadRequest("O campo termos de uso deve conter apenas os caracteres 'Y' ou 'N'");

            if (model.Period != "Manhã" && model.Period != "Tarde" && model.Period != "Noite"  && model.Period != "Integral")
                return BadRequest("Não é permitido valores diferentes do padrão para o campo Period (Manhã, Tarde, Noite ou Integral)");

            var insertService = new ImportBigDataService();

            var response = await insertService.InsertBigDataOnDataBase(userModel.UserName, model.CompleteName, model.BornDate,
                model.CPF, model.RG, model.Adress, model.Number, model.Neighborhood, model.TeachingInstitution,
                model.HaveBF, model.HaveCadUniq, model.CityTeachingInstitution, model.Period, model.TermsOfUse, model.MonthStudy);

            string failResponse = "Falha na inserção dos Dados.";
            string successResponse = "Sucesso na Inserção!";

            if (response == "UserNotFound")
            {
                Log.Error(string.Format("[HttpPost] Usuário não encontrado ({0}) com uma conta criada.", userModel.UserName));
                return BadRequest(new { Message = string.Format("{0} Usuário não encontrado ({1}) com uma conta criada.", failResponse, userModel.UserName) });
            }
            else if (response == "PeriodExists")
            {
                Log.Error(string.Format("[HttpPost] Falha na inserção de documentos para o usuário: {0} no mês {1} (Mês já existe para este usuário)",
                    userModel.UserName, model.MonthStudy));
                return BadRequest(new { Message = string.Format("{0} mês com documentos já existentes para este usuário.", failResponse) });
            }
            else if (response == "DatabaseFailure")
            {
                Log.Error(string.Format("[HttpPost] Falha com o banco de dados {0} para o usuário {1}", response, userModel.UserName));
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Falha ao inserir dados no banco de dados." });
            }
            else if (response == "Sucssess")
            {
                Log.Information("Sucesso na inserção");
                return Ok(new { Message = successResponse });
            }
            else
            {
                Log.Error(string.Format("Falha na inserção. Exception {0}", response));
                return BadRequest(new { Message = string.Format("Falha na inserção de dados no banco. Exception: {0}", response) });
            }
        }

        [HttpPut]
        [Route("updateData")]
        [Authorize]
        public async Task<dynamic> UpdateBigData([FromQuery] UserVerificationModel userModel, [FromBody] BigDataModelUpdate model)
        {
            if (model.Adress.Length > 100) return BadRequest("Campo de Adress não pode ter mais que 100 caracteres");
            if (model.Number.Length > 10) return BadRequest("Campo de Number não pode ter mais que 10 caracteres");
            if (model.Neighborhood.Length > 100) return BadRequest("Campo de Neighborhood não pode ter mais que 100 caracteres");
            if (model.TeachingInstitution.Length > 100) return BadRequest("Campo de TeachingInstitution não pode ter mais que 100 caracteres");
            if (model.HaveBF.Length > 1) return BadRequest("Campo de HaveBF não pode ter mais que 1 caracter");
            if (model.HaveCadUniq.Length > 1) return BadRequest("Campo de HaveCadUniq não pode ter mais que 1 caracter");
            if (model.CityTeachingInstitution.Length > 100) return BadRequest("Campo de CityTeachingInstitution não pode ter mais que 100 caracteres");
            if (model.Period.Length > 50) return BadRequest("Campo de Period não pode ter mais que 50 caracteres");
            if (model.TermsOfUse.Length > 1) return BadRequest("O campo termos de uso não pode conter mais que 1 caracter (Y, N)");
            if (model.MonthStudy.Length > 50) return BadRequest("O campo MonthStudy não pode conter mais que 50 caracteres");

            if (model.TermsOfUse != "Y" && model.TermsOfUse != "N")
                return BadRequest("O campo termos de uso deve conter apenas os caracteres 'Y' ou 'N'");

            if (model.Period != "Manhã" && model.Period != "Tarde" && model.Period != "Noite" && model.Period != "Integral")
                return BadRequest("Não é permitido valores diferentes do padrão para o campo Period (Manhã, Tarde, Noite ou Integral)");

            var updateService = new ImportBigDataService();

            var response = await updateService.UpdateBigDataOnDataBase(userModel.UserName, model.Adress, model.Number, model.Neighborhood, model.TeachingInstitution,
                model.HaveBF, model.HaveCadUniq, model.CityTeachingInstitution, model.Period, model.TermsOfUse, model.MonthStudy);

            string failResponse = "Falha no Update dos Dados.";
            string successResponse = "Sucesso na Atualização!";

            if (response == "UserNotFound")
            {
                Log.Error(string.Format("[HttpPost] Usuário não encontrado ({0}) com uma conta criada.", userModel.UserName));
                return BadRequest(new { Message = string.Format("{0} Usuário não encontrado ({1}) com uma conta criada.", failResponse, userModel.UserName) });
            }
            else if (response == "DatabaseFailure")
            {
                Log.Error(string.Format("[HttpPost] Falha com o banco de dados {0} para o usuário {1}", response, userModel.UserName));
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Falha ao inserir dados no banco de dados." });
            }
            else if (response == "Sucssess")
            {
                Log.Information("Sucesso na atualização");
                return Ok(new { Message = successResponse });
            }
            else
            {
                Log.Error(string.Format("Falha na atualização. Exception {0}", response));
                return BadRequest(new { Message = string.Format("Falha na atualização de dados no banco. Exception: {0}", response) });
            }
        }

        [HttpPut]
        [Route("updateColor")]
        [Authorize]
        public async Task<dynamic> UpdateColorTable([FromBody] ColorManager model)
        {
            var query = string.Format("UPDATE colors SET colors.ColorOfMonth = '{0}' WHERE colors.FirstDay = '{1}'", model.ColorOfMonth, model.Month);

            var connectionToCreate = new MySQLConnection();
            var finalResult = connectionToCreate.connectionDataBase(query).ToString();

            if (string.IsNullOrEmpty(finalResult))
            {
                return new { Message = "Falha ao atualizar cor." };
            }
            else
            {
                return new { Message = "Cor alterada com sucesso." };
            }
        }
    }
}