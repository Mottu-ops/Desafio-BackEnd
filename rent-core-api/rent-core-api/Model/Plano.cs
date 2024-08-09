using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace rent_core_api.Model
{
    [Table("plano")]
    public class Plano
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("dias")]
        public int Dias { get; set; }
        [Column("valor_diario")]
        public decimal ValorDiario { get; set; }
    }
}
