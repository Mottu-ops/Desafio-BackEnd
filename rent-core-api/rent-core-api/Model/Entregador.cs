using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rent_core_api.Model
{
    [Table("entregador")]
    public class Entregador
    {
        [Key]
        public int id { get; set; }

        [Required]
        [MaxLength(100)]
        public string nome { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 14)]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "O CNPJ deve ter 14 dígitos.")]
        public string cnpj { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column("data_nascimento")]
        public DateOnly dataNascimento { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)]
        [Column("numero_cnh")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O número da CNH deve ter 11 dígitos.")]
        public string numeroCnh { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 1)]
        [Column("tipo_cnh")]
        [RegularExpression(@"^(A|B|A\+B)$", ErrorMessage = "Tipo de CNH deve ser 'A', 'B' ou 'AB'.")]
        public string tipoCnh { get; set; }

        [Column("url_img_cnh")]
        public string imagemCnh { get; set; }

        public Entregador(string nome, string cnpj, DateOnly dataNascimento, string numeroCnh, string tipoCnh, string imagemCnh)
        {
            this.nome = nome ?? throw new ArgumentNullException(nameof(nome));
            this.cnpj = cnpj ?? throw new ArgumentNullException(nameof(cnpj));
            this.dataNascimento = dataNascimento;
            this.numeroCnh = numeroCnh ?? throw new ArgumentNullException(nameof(numeroCnh));
            this.tipoCnh = tipoCnh ?? throw new ArgumentNullException(nameof(tipoCnh));
            this.imagemCnh = imagemCnh ?? throw new ArgumentNullException(nameof(imagemCnh));
        }
        public Entregador()
        {

        }
    }
}
