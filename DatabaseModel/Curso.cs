using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseModel
{
    public class Curso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CursoId {get; set;}

        public string Nome { get; set; }
        public string CodigoSisu { get; set; }

        public virtual List<GrauTurno> GrausTurnos { get; set; }

        public int LocalOfertaId { get; set; }

        [ForeignKey("LocalOfertaId")]
        public virtual LocalOferta LocalOferta { get; set; }
    }
}
