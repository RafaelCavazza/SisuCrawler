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

        public virtual GrauTurno GrauTurno { get; set; }
    }
}
