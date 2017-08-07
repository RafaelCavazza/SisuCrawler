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

        public int SolicitacaoDadosId { get; set; }

        [ForeignKey("SolicitacaoDadosId")]
        public virtual SolicitacaoDados SolicitacaoDados { get; set; }
        public virtual List<LocalOferta> LocaisOferta { get; set; }
    }
}
