using BackEndAplication.Models;
using BackEndAplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var response = await insertService.InsertBigDataOnDataBase(model.user, model.CompleteName, model.BornDate,
                model.CPF, model.RG, model.Adress, model.Number, model.Neighborhood, model.TeachingInstitution,
                model.HaveBF, model.HaveCadUniq, model.CityTeachingInstitution, model.Period);

            string failResponse = "Falha ao Importar Dados.";
            string successResponse = "Sucesso na Inserção!";

            if (response)
            {
                return Ok(successResponse);
            }
            else
            {
                return BadRequest(failResponse);
            }
        }
    }
}