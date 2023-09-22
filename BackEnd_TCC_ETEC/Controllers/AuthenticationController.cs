using BackEnd_TCC_ETEC.Services;
using BackEndAplication.Models;
using BackEndAplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_TCC_ETEC.Controllers
{
    [ApiController]
    [Route("/account")]
    public class AuthenticationController : Controller
    {
        // ENDPOINT - Login
        [HttpPost]
        [Route("/login")]
        public IActionResult Auth(string username, string password)
        {
            if (username == "igor.afonso" && password == "123456")
            {
                var token = TokenService.GenerateToken(username);
                return Ok(token);
            }

            return BadRequest("Usuário ou senha Inválido");
        }

        // ENDPOINT - Criar Usuário
        [HttpPost]
        [Route("/register")]
        public async Task<ActionResult<dynamic>> Register([FromBody] CreateUserModel model)
        {

            var constructorNewUser = new CreateUserService();
            try
            {
                var user = await constructorNewUser.CreateUser(model.Username, model.Password, model.email);
                if (user != null)
                {
                    return new SimplifiedResponseViewModel()
                    {
                        Data = DateTime.Now,
                        Mensagem = user
                    };
                }
                else
                {
                    return new SimplifiedResponseViewModel()
                    {
                        Data = DateTime.Now,
                        Mensagem = "Falha ao criar usuário."
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseViewModel()
                {
                    Data = ex.Data,
                    Sucesso = false,
                    Mensagem = ex.Message
                };
            }
        }

        //ENDPOINT - Recuperação de Senha
        [HttpPost]
        [Route("/recovery")]
        public async Task<ActionResult<dynamic>> PasswordRecover([FromBody] UserLogin model)
        {
            string message = "";
            var objectRecovery = new RecoverPasswordServicecs();
            var result = await objectRecovery.RecoverPassword(model.UserName, model.Password);

            if (string.IsNullOrEmpty(result))
            {
                message = "Não Foi Possível Recuperar a senha";
            }
            else
            {
                message = result;
            }

            return new SimplifiedResponseViewModel()
            {
                Data = DateTime.Now,
                Mensagem = message
            };
        }
    }
}
