using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseModel
{
    public class GrauTurno
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GrauTurnoId { get; set; }

        public string Nome { get; set; }
        public string CodigoSisu { get; set; }

        public virtual List<Aluno> Alunos { get; set;}
    }
}
