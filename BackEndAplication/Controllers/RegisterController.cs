using BackEndAplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndAplication.Controllers
{
    [Route("/account/create")]
    public class RegisterController : Controller
    {
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Register([FromBody] Users model)
        {
            //var user = await CreateUser(model.Username, model.Password, model.email);

            //if (user == null)
            //    return BadRequest(new { message = "Não Foi possível Criar o usuário." });
            return "";
        }
    }
}
