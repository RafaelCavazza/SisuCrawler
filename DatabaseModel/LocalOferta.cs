using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseModel
{
    public class LocalOferta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocalOfertaId { get; set; }

        public string Nome { get; set; }
        public string CodigoSisu { get; set; }
        public virtual List<Curso> Cursos { get; set; }

        public int UniversidadeId { get; set; }

        [ForeignKey("UniversidadeId")]
        public virtual Universidade Universidade { get; set; }
    }
}
