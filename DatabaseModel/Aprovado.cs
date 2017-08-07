using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseModel
{
    public class Aprovado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AprovadoId { get; set; }

        public string Nome { get; set; }
        public string Nota { get; set; }
        public string Inscricao { get; set; }
        public int Classificacao { get; set; }
        public string TipoConcorrencia { get; set; }
        public int GrauTurnoId { get; set; }

        [ForeignKey("GrauTurnoId")]
        public virtual GrauTurno GrauTurno { get; set; }
    }
}
