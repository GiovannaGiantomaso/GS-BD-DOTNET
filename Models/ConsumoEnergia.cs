using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalSolution.Models
{
    [Table("CONSUMO_ENERGIA")]
    public class ConsumoEnergia
    {
        [Key]
        [Column("ID_CONSUMO")]
        public int IdConsumo { get; set; }

        [Required]
        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column("DATA_REGISTRO")]
        public DateTime DataRegistro { get; set; }

        [Required]
        [Column("CONSUMO_KWH", TypeName = "NUMBER(10, 2)")]
        public decimal ConsumoKwh { get; set; }
    }
}
