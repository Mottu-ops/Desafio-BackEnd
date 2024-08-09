using Microsoft.AspNetCore.Mvc;
using rent_core_api.Model;
using rent_core_api.Service;
using rent_core_api.ViewModel;

namespace rent_core_api.Controllers
{
    [ApiController]
    [Route("api/v1/deliveryman")]
    public class EntregadorController : ControllerBase
    {
        private readonly EntregadorService _entregadorService;
        public EntregadorController(EntregadorService entregadorService)
        {
            this._entregadorService = entregadorService ?? throw new ArgumentNullException(nameof(entregadorService));
        }

        [HttpPost]
        public IActionResult Add ([FromForm] EntregadorRequest entregadorRequest)
        {
            var filePath = "";

            if (entregadorRequest.photo != null) 
            {
                var extension = Path.GetExtension(entregadorRequest.photo.FileName).ToLower();
 
                if (extension != ".png" && extension != ".bmp")
                    return BadRequest("Formato de imagem inválido. Somente PNG e BMP são suportados.");

                filePath = Path.Combine("Storage", entregadorRequest.photo.FileName);
                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                entregadorRequest.photo.CopyTo(fileStream);
            }

            var entregador = new Entregador(entregadorRequest.nome,
                entregadorRequest.cnpj,
                entregadorRequest.dataNascimento,
                entregadorRequest.numeroCnh,
                entregadorRequest.tipoCnh,
                filePath);

            _entregadorService.Add(entregador);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCadastroEntregador(int id, [FromForm] EntregadorPhotoRequest entregadorPhoto)
        {
            if(entregadorPhoto.Photo == null)
                return BadRequest("Nenhuma foto informada.");

            var entregador = _entregadorService.GetById(id);

            if (entregador == null)
                return NotFound("Nenhum entregador encontrado.");

            try
            {
                var extension = Path.GetExtension(entregadorPhoto.Photo.FileName).ToLower();

                if (extension != ".png" && extension != ".bmp")
                    return BadRequest("Formato de imagem inválido. Somente PNG e BMP são suportados.");

                var filePath = Path.Combine("Storage", entregadorPhoto.Photo.FileName);
                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                entregadorPhoto.Photo.CopyTo(fileStream);

                entregador.imagemCnh = filePath;
                _entregadorService.Update(entregador);
                
                return Ok(entregador);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
