using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalSolution.Models
{
    [Table("HISTORICO_CONSUMO")]
    public class HistoricoConsumo
    {
        [Key]
        [Column("ID_HISTORICO")]
        public int IdHistorico { get; set; }

        [Required]
        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Required]
        [Column("TOTAL_CONSUMO", TypeName = "NUMBER(10, 2)")]
        public decimal TotalConsumo { get; set; }

        [Required]
        [Column("MEDIA_MENSAL", TypeName = "NUMBER(10, 2)")]
        public decimal MediaMensal { get; set; }
    }
}
