using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace rent_core_api.Model
{
    [Table("locacao")]
    public class Locacao
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("id_moto")]
        public long IdMoto { get; set; }
        [Column("id_entregador")]
        public long IdEntregador { get; set; }
        [Column("id_plano")]
        public int IdPlano { get; set; }
        [Column("data_inicial")]
        public string? DataInicial { get; set; }
        [Column("data_fim")]
        public string? DataFim { get; set; }
        [Column("data_previsao")]
        public string? DataPrevisao { get; set; }
        [Column("data_devolucao")]
        public string? DataDevolucao { get; set; }
        [Column("valor_total")]
        public decimal ValorTotal { get; set; }
        [Column("multa")]
        public decimal? Multa { get; set; }
    }
}
