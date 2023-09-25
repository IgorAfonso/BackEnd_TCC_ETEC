using BackEnd_TCC_ETEC.Services;
using BackEndAplication.Data;
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
        public IActionResult Auth([FromBody] UserLogin model)
        {
            var hashService = new HashGenerator();
            var hashedPassword = hashService.HashCreator(model.Password);

            var mConn = new MySQLConnectionWithValue();
            var query = string.Format("SELECT users.username, users.Password FROM users WHERE users.username = '{0}'", model.UserName);
            var UserDB = mConn.ConsultUser(query);

            if (string.IsNullOrEmpty(UserDB.ToString()))
                return BadRequest("Usuário não existente");


            var userAndPass = mConn.TrueUser(query, model.UserName, hashedPassword).Result;

            if (userAndPass)
            {
                var token = TokenService.GenerateToken(model.UserName);
                return Ok(token);
            }
            else
            {
                return BadRequest("Usuário ou senha Inválido");
            }
        }

        // ENDPOINT - Criar Usuário
        [HttpPost]
        [Route("/register")]
        public async Task<ActionResult<dynamic>> Register([FromBody] CreateUserModel model)
        {
            var hashService = new HashGenerator();
            var hashedPassword = hashService.HashCreator(model.Password);

            var constructorNewUser = new CreateUserService();
            try
            {
                var user = await constructorNewUser.CreateUser(model.Username, hashedPassword, model.email);
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
        [HttpPut]
        [Route("/recovery")]
        public async Task<ActionResult<dynamic>> PasswordRecover([FromBody] UserLogin model)
        {
            var hashService = new HashGenerator();
            var hashedPassword = hashService.HashCreator(model.Password);

            string message = "";
            var objectRecovery = new RecoverPasswordServicecs();
            var result = await objectRecovery.RecoverPassword(model.UserName, hashedPassword);

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
