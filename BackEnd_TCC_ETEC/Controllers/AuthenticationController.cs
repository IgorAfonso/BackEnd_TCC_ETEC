using BackEnd_TCC_ETEC.Services;
using BackEndAplication.Data;
using BackEndAplication.Models;
using BackEndAplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
            {
                Log.Error(string.Format("[HttpPost] Usuário {0} não existe nos registros.", model.UserName));
                return BadRequest( new { Message = "Usuário não existente" });
            }

            var userAndPass = mConn.TrueUser(query, model.UserName, hashedPassword).Result;

            if (userAndPass)
            {
                Log.Information(string.Format("[HttpPost] Usuário {0} autenticado com sucesso!", model.UserName));
                var token = TokenService.GenerateToken(model.UserName);
                Log.Information(string.Format("[HttpPost] Token Gerado para o usuário: {0}", model.UserName));
                return Ok(token);
            }
            else
            {
                Log.Error(string.Format("[HttpPost] Usuário ou senha Inválido.{0}", model.UserName));
                return BadRequest(new { Message = "Usuário ou senha Inválido" });
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
                    Log.Information(string.Format("[HttpPost] Usuário {0} criado com sucesso!", model.Username));
                    return new SimplifiedResponseViewModel()
                    {
                        Data = DateTime.Now,
                        Mensagem = user
                    };
                }
                else
                {
                    Log.Error(string.Format("[HttpPost] Falha na criação do usuário: {0}", model.Username));
                    return new SimplifiedResponseViewModel()
                    {
                        Data = DateTime.Now,
                        Mensagem = "Falha ao criar usuário."
                    };
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("[HttpPost] Erro ao criar usuário Exception: {0}", ex.Message));
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
                Log.Error(string.Format("[HttpPut] Não Foi possível recuperar a senha para o usuário: {0}", model.UserName));
                message = "Não Foi Possível Recuperar a senha";
            }
            else
            {
                Log.Information(string.Format("[HttpPut] Senha recuperada com sucesso para o usuário {0}", model.UserName));
                message = result;
            }

            return new SimplifiedResponseViewModel()
            {
                Data = DateTime.Now,
                Mensagem = message
            };
        }

        // EndPoint para deletar usuário
        [HttpDelete]
        [Route("/deleteUser")]
        [Authorize]
        public IActionResult DeleteUser([FromQuery] UserVerificationModel model)
        {
            var conn = new MySQLConnectionWithValue();
            var authentiatedUserQuery = string.Format("SELECT ID FROM users WHERE users.username = '{0}'", model.UserName);
            var validation = conn.ValidateExistingUser(authentiatedUserQuery);

            if (validation.Result == null || validation.Result == "DatabaseFailure")
            {
                Log.Error(string.Format("Usuário não encontrado nos registros: {0}", model.UserName));
                return BadRequest(new { message = "Não foi possível encontrar o usuário" });
            }

            var deleteService = new DeleteUserService();
            var deleteServiceExecution = deleteService.DeleteUser(validation.Result);

            if (deleteServiceExecution == null || deleteServiceExecution == "DatabaseFailure")
                return BadRequest(new { message = "Falha ao Deletar usuário."});

            return Ok(new { message = deleteServiceExecution});
        }
    }
}
