using Microsoft.AspNetCore.Mvc;
using rent_core_api.Service;

namespace rent_core_api.Controllers
{
    [ApiController]
    [Route("api/v1/rental")]
    public class LocacaoController : ControllerBase
    {
        private readonly LocacaoService _locacaoService;
        public LocacaoController(LocacaoService locacaoService)
        {
            this._locacaoService = locacaoService ?? throw new ArgumentNullException(nameof(locacaoService));
        }

        [HttpPost]
        public IActionResult CriarLocacao(int idMoto, int idEntregador, int idPlano)
        {
            try
            {
                var locacao = _locacaoService.CriarLocacao(idMoto, idEntregador, idPlano);
                return Ok(locacao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{idLocacao}")]
        public IActionResult FinalizarLocacao(int idLocacao, DateTime dataDevolucao)
        {
            try
            {
                var locacao = _locacaoService.FinalizarLocacao(idLocacao, dataDevolucao);
                return Ok(locacao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
