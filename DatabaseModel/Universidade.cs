using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseModel
{
    public class Universidade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UniversidadeId { get; set; }

        public string Nome { get; set; }

        public virtual List<LocalOferta> LocaisOferta { get; set; }
    }
}
