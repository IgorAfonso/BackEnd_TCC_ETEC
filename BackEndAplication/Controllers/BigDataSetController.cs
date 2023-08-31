using BackEndAplication.Models;
using BackEndAplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndAplication.Controllers
{
    public class BigDataSetController : Controller
    {
        [HttpPost]
        [Route("importData")]
        [Authorize]
        public async Task<ActionResult<dynamic>> ImportDataSet([FromBody] bigDataModel model)
        {
            var userAuthenticated = User.Identity.Name;
            if(userAuthenticated == null)
            {
                var autFailResponse = "Falha ao encontrar usupario Authenticado.";
                return BadRequest(autFailResponse);
            }
            else
            {
                var insertService = new ImportBigDataService();
                var response = await insertService.InsertBigDataOnDataBase(userAuthenticated, model.CompleteName, model.BornDate,
                    model.CPF, model.RG, model.Adress, model.Number, model.Neighborhood, model.TeachingInstitution,
                    model.HaveBF, model.HaveCadUniq, model.CityTeachingInstitution, model.Period);

                string failResponse = "Falha ao Importar Dados.";

                if (response)
                {
                    return Results.StatusCode(200).ToString();
                }
                else
                {
                    return BadRequest(failResponse);
                }
            }
        }
    }
}
