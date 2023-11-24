using BackEnd_TCC_ETEC.Models;
using BackEnd_TCC_ETEC.Services;
using BackEndAplication.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using Serilog;
using static System.Net.Mime.MediaTypeNames;

namespace BackEnd_TCC_ETEC.Controllers
{
    public class ImageController : Controller
    {
        [HttpPost]
        [Route("/insertImage")]
        [Authorize]
        public IActionResult InsertImage([FromBody]ImageModel model)
        {
            var conn = new MySQLConnectionWithValue();
            var authentiatedUserQuery = string.Format("SELECT ID FROM users WHERE users.username = '{0}'", model.UserName);
            var validation = conn.ValidateExistingUser(authentiatedUserQuery);

            if (validation.Result == null || validation.Result == "DatabaseFailure")
            {
                Log.Error(string.Format("Usuário não encontrado nos registros: {0}", model.UserName));
                return BadRequest(new { message = "Não foi possível encontrar o usuário" });
            }

            if(string.IsNullOrEmpty(model.Image) || model.Image == string.Empty)
            {
                Log.Error(string.Format("Imagem não enviada para o usuário: {0}", model.UserName));
                return BadRequest(new { message = "Campo imagem obrigatório" });
            }

            var instance = new ImageService();
            var imageServiceResult = instance.InsertImageService(model.UserName, model.Image);

            if(imageServiceResult == "Falha ao Inserir Imagem no Banco de Dados")
                return BadRequest(new { Message = imageServiceResult });

            return Created("", new { Message = imageServiceResult});
        }

        [HttpPut]
        [Route("/updateImage")]
        [Authorize]
        public IActionResult UpdateImage([FromBody] ImageModel model)
        {
            var conn = new MySQLConnectionWithValue();
            var authentiatedUserQuery = string.Format("SELECT ID FROM users WHERE users.username = '{0}'", model.UserName);
            var validation = conn.ValidateExistingUser(authentiatedUserQuery);

            if (validation.Result == null || validation.Result == "DatabaseFailure")
            {
                Log.Error(string.Format("Usuário não encontrado nos registros: {0}", model.UserName));
                return BadRequest(new { message = "Não foi possível encontrar o usuário" });
            }

            if (string.IsNullOrEmpty(model.Image) || model.Image == string.Empty)
            {
                Log.Error(string.Format("Imagem não enviada para o usuário: {0}", model.UserName));
                return BadRequest(new { message = "Campo imagem obrigatório" });
            }

            var instance = new ImageService();
            var imageServiceResult = instance.UpdateImageService(model.UserName, model.Image);

            if (imageServiceResult == "Falha ao Atualizar Imagem no Banco de Dados")
                return BadRequest(new { Message = imageServiceResult });

            return Ok(new { Message = imageServiceResult });
        }

        [HttpGet]
        [Route("/getImage")]
        [Authorize]
        public IActionResult GetImage([FromQuery] string UserName)
        {
            var conn = new MySQLConnectionWithValue();
            var authentiatedUserQuery = string.Format("SELECT ID FROM users WHERE users.username = '{0}'", UserName);
            var validation = conn.ValidateExistingUser(authentiatedUserQuery);

            if (validation.Result == null || validation.Result == "DatabaseFailure")
            {
                Log.Error(string.Format("Usuário não encontrado nos registros: {0}", UserName));
                return BadRequest(new { message = "Não foi possível encontrar o usuário" });
            }

            var instance = new ImageService();
            var imageResult = instance.GetImageService(UserName);

            if (string.IsNullOrEmpty(imageResult))
                return BadRequest(new ImageModel 
                { 
                    UserName = UserName,
                    Image = ""
                });

            return Ok(new ImageModel
            {
                UserName = UserName,
                Image = imageResult
            });
        }
    }
}
