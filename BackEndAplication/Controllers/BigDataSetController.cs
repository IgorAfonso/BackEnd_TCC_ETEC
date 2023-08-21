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
        public async Task<ActionResult<dynamic>> ImportDataSet([FromBody] Users model)
        {
            var constructorNewUser = new CreateUserService();
            var user = await constructorNewUser.CreateUser(model.Username, model.Password, model.email);

            if (user == null)
            {
                return BadRequest(new { message = "Não Foi possível Criar o usuário." });
            }
            else
            {
                return Results.StatusCode(200).ToString();
            }
                
        }
    }
}
