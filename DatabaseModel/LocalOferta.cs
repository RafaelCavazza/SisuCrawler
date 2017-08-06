using System.Collections.Generic;

namespace DatabaseModel
{
    public class LocalOferta
    {
        public int LocalOfertaId { get; set; }
        public string Nome { get; set; }
        public string CodigoSisu { get; set; }
        public List<Curso> Cursos { get; set; }
    }
}
