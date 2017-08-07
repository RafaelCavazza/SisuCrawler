using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseModel
{
    public class SolicitacaoDados
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SolicitacaoDadosId { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual List<Universidade> Universidades { get; set; }
    }
}
