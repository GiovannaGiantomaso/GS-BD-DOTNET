using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalSolution.Models
{
    [Table("USUARIO_ENERGIA")]
    public class UsuarioEnergia
    {
        [Key]
        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Required]
        [Column("NOME")]
        public string Nome { get; set; }

        [Required]
        [Column("EMAIL")]
        public string Email { get; set; }

        [Required]
        [Column("SENHA")]
        public string Senha { get; set; }

        public ICollection<ConsumoEnergia> Consumos { get; set; }
        public ICollection<HistoricoConsumo> HistoricosConsumo { get; set; }
        public ICollection<FeedbackConsumo> FeedbacksConsumo { get; set; }
    }
}
