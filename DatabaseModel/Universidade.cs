using System.Collections.Generic;

namespace DatabaseModel
{
    public class Universidade
    {
        public string Nome { get; set; }

        public List<LocalOferta> LocaisOferta { get; set; }
    }
}
