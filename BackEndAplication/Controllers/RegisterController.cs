using BackEndAplication.Models;
using BackEndAplication.Services;
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
        public async Task<ActionResult<dynamic>> Register([FromBody] CreateUserModel model)
        {
            
            var constructorNewUser = new CreateUserService();
            try
            {
                var user = await constructorNewUser.CreateUser(model.Username, model.Password, model.email);
                if(user != null)
                {
                    var response = new ResponseViewModel()
                    {
                        Data = DateTime.Now,
                        Sucesso = true,
                        Mensagem = "Sucesso ao cadastrar usuário."
                    };
                    return Json(response);
                }
                else
                {
                    var response = new ResponseViewModel()
                    {
                        Data = DateTime.Now,
                        Sucesso = false,
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
    }
}
