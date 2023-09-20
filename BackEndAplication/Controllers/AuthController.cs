using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BackEndAplication.Models;
using BackEndAplication.Services;

namespace BackEndAplication.Controllers
{
    [Route("/account/authenticate")]
    public class HomeController : Controller
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserLogin model)
        {
            var connectionDatabase = new Data.MySQLConnectionWithValue();
            var user = connectionDatabase.ConsultUser(string.Format("SELECT ID, username, Password, email " +
                "FROM USERS WHERE username = '{0}' AND Password = '{1}' LIMIT 1", model.UserName, model.Password));

            //var user = UserRepository.Get(model.Username, model.Password);

            if (string.IsNullOrEmpty(user.ToString()))
                return NotFound(new { message = "Usuário ou senha inválidos" });

            string token = TokenService.GenerateToken(user.ToString());
            //model.Password = "";
            return new Models.ReturnLoginModel
            {
                Token = token.ToString()
            };
        }
    }
}