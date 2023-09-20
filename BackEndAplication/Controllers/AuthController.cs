using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BackEndAplication.Models;
using BackEndAplication.Services;
using System.ComponentModel;

namespace BackEndAplication.Controllers
{
    [DisplayName("Account")]
    [Route("/account/")]
    public class Account : Controller
    {
        // ENDPOINT - LOGIN
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserLogin model)
        {
            var connectionDatabase = new Data.MySQLConnectionWithValue();
            var user = connectionDatabase.ConsultUser(string.Format("SELECT ID, username, Password, email " +
                "FROM USERS WHERE username = '{0}' AND Password = '{1}' LIMIT 1", model.UserName, model.Password));

            if(user.Result != model.UserName)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            //if (string.IsNullOrEmpty(user.ToString()))
                

            try
            {
                string token = TokenService.GenerateToken(user.ToString());
                return new Models.ReturnLoginModel
                {
                    UserName = user.Result,
                    Token = token.ToString()
                };
            }
            catch (Exception)
            {
                return new ReturnLoginModel
                {
                    UserName = user.Result,
                    Exception = "Falha ao fazer Login"
                };
            }
            
        }

        // ENDPOINT - Criar Usuário
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Register([FromBody] CreateUserModel model)
        {

            var constructorNewUser = new CreateUserService();
            try
            {
                var user = await constructorNewUser.CreateUser(model.Username, model.Password, model.email);
                if (user != null)
                {
                    var response = new SimplifiedResponseViewModel()
                    {
                        Data = DateTime.Now,
                        Mensagem = user
                    };
                    return Json(response);
                }
                else
                {
                    var response = new SimplifiedResponseViewModel()
                    {
                        Data = DateTime.Now,
                        Mensagem = "Falha ao criar usuário."
                    };
                    return Json(response);
                }

            }
            catch (Exception ex)
            {
                var response = new ResponseViewModel()
                {
                    Data = ex.Data,
                    Sucesso = false,
                    Mensagem = ex.Message
                };
                return Json(response);
            }
        }

        //ENDPOINT - Recuperação de Senha
        [HttpPost]
        [Route("/recovery")]
        [AllowAnonymous]
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

            var response = new SimplifiedResponseViewModel()
            {
                Data = DateTime.Now,
                Mensagem = message
            };

            return Json(response);
        }
    }
}