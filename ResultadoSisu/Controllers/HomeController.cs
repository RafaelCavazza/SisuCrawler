using DatabaseModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ResultadoSisu.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(HttpPostedFileBase arquivo)
        {
            if(arquivo != null)
            {
                using (var db = new DadosSisu())
                {
                    var alunos = GetAlunos(arquivo);
                    var aprovados = db.Aprovados
                        .Include("GrauTurno")
                        .Include("GrauTurno.Curso")
                        .Include("GrauTurno.Curso.LocalOferta")
                        .Include("GrauTurno.Curso.LocalOferta.Universidade")
                        .Where(a => alunos.Any(al => a.Nome.Contains(al)))
                        .ToList();

                    ViewBag.Alunos = alunos;
                    ViewBag.Aprovados = aprovados;
                }
            }

            return View();
        }

        private List<string> GetAlunos(HttpPostedFileBase arquivo)
        {
            var txt = new StreamReader(arquivo.InputStream).ReadToEnd();
            var nomesTratados = txt.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
                .Select(
                    s => Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(s.ToUpper())))
                .ToList();

            return nomesTratados;
        }
    }
}