using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace rent_core_api.ViewModel
{
    public class EntregadorRequest
    {
        public string nome { get; set; }
        public string cnpj { get; set; }
        public DateOnly dataNascimento { get; set; }
        public string numeroCnh { get; set; }
        public string tipoCnh { get; set; }
        public IFormFile photo { get; set; }
    }
}
