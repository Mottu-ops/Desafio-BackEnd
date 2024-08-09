using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rent_core_api.Model
{
    [Table("moto")]
    public class Moto
    {
        [Key]
        public int id {  get; set; }
        public int ano { get; set; }
        public string ? modelo { get; set; }

        [Required(ErrorMessage = "A placa é obrigatória.")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "A placa deve ter exatamente 7 caracteres.")]
        public string placa { get; set; }

        public Moto(int ano, string modelo, string placa) {
            this.ano = ano;
            this.modelo = modelo ?? throw new ArgumentNullException(nameof(modelo));
            this.placa = placa ?? throw new ArgumentNullException(nameof(placa)); 
        }
    }
}
