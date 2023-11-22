using BackEnd_TCC_ETEC.Models;
using BackEnd_TCC_ETEC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
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
