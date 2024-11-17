using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalSolution.Models
{
    [Table("FEEDBACK_CONSUMO")]
    public class FeedbackConsumo
    {
        [Key]
        [Column("ID_FEEDBACK")]
        public int IdFeedback { get; set; }

        [Required]
        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Required]
        [Column("MENSAGEM_FEEDBACK")]
        public string MensagemFeedback { get; set; }
    }
}
