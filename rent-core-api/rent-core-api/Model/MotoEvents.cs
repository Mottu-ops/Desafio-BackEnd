using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rent_core_api.Model
{
    [Table("moto_events")]
    public class MotoEvents
    {
        [Key]
        public int id { get; set; }
        public string mensagem { get; set; }
        public string placa { get; set; }

        public MotoEvents(String mensagem, string placa)
        {
            this.mensagem = mensagem ?? throw new ArgumentNullException(nameof(mensagem));
            this.placa = placa ?? throw new ArgumentNullException(nameof(placa));
        }
    }
}
