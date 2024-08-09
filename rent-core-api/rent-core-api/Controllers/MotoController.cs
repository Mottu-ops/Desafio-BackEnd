using Microsoft.AspNetCore.Mvc;
using rent_core_api.Model;
using rent_core_api.Service;
using rent_core_api.ViewModel;

namespace rent_core_api.Controllers
{
    [ApiController]
    [Route("api/v1/motorcycle")]
    public class MotoController : ControllerBase
    {
        private readonly MotoService _motoService;
        public MotoController(MotoService motoService)
        {
            this._motoService = motoService ?? throw new ArgumentNullException(nameof(motoService));
        }

        [HttpPost]
        public IActionResult Add(MotoRequest motoRequest)
        {
            var moto = new Moto(motoRequest.ano, motoRequest.modelo, motoRequest.placa);
            _motoService.Add(moto);
            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var motos = _motoService.GetAll();
            return Ok(motos);
        }

        [HttpGet("{placa}")]
        public IActionResult GetByPlaca(string placa)
        {
            var moto = _motoService.GetByPlaca(placa);
            
            if (moto == null)
                return NotFound();
            
            return Ok(moto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlaca(int id, [FromBody] string novaPlaca)
        {
            if(novaPlaca.Length != 7)
                return BadRequest("A placa deve ter exatamente 7 caracteres.");

            try{
                var moto = _motoService.GetById(id);
                if (moto == null){
                    return NotFound();
                }

                moto.placa = novaPlaca.ToUpper();
                _motoService.Update(moto);

                return Ok(moto);
            }
            catch (ArgumentNullException ex){
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex){
                return BadRequest(ex.Message);
            }
            catch (Exception ex){
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverMoto(int id)
        {
            try
            {
                _motoService.Delete(id);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
