using System.Collections.Generic;

namespace DatabaseModel
{
    public class Curso
    {
        public string Nome { get; set; }
        public string CodigoSisu { get; set; }
        public List<GrauTurno> Cursos { get; set; }
    }
}
