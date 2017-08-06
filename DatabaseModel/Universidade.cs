using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModel
{
    public class Universidade
    {
        public string Nome { get; set; }

        public List<LocalOferta> LocaisOferta { get; set; }
    }
}
