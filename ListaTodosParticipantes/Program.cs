using DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ListaTodosParticipantes
{
    class Program
    {
        static void Main(string[] args)
        {
            var dadosSisu = new DadosSisu();
            var listagemSisu = new ListagemSisu();

            var universidadesPendentes = dadosSisu.Universidades
                .Include("LocaisOferta")
                .Include("LocaisOferta.Cursos")
                .Include("LocaisOferta.Cursos.GrausTurnos")
                .Include("LocaisOferta.Cursos.GrausTurnos.Aprovados")
                .Where(un => un.LocaisOferta.Any() == false).ToList();

            dadosSisu.Dispose();

            if (universidadesPendentes.Any())
            {
                foreach (var universidade in universidadesPendentes)
                {
                    using (var context = new DadosSisu())
                    {
                        var uni = context.Universidades.Find(universidade.UniversidadeId);
                        listagemSisu.GetOpcoesUniversidade(uni);
                        listagemSisu.GetAprovadosUniversidade(uni);
                        context.SaveChanges();
                    }
                }
            }
            else
            {
                using (var context = new DadosSisu())
                {
                    var solicitacaoDados = new SolicitacaoDados()
                    {
                        CreatedOn = DateTime.Now,
                        Universidades = new List<Universidade>()
                    };
                    context.SolicitacaoDados.Add(solicitacaoDados);

                    context.SaveChanges();

                    var universidades = listagemSisu.GetUniversidades();
                    solicitacaoDados.Universidades.AddRange(universidades);

                    context.SaveChanges();

                    foreach (var universidade in universidades)
                    {
                        universidade.SolicitacaoDados = solicitacaoDados;
                        listagemSisu.GetOpcoesUniversidade(universidade);
                        listagemSisu.GetAprovadosUniversidade(universidade);
                        context.SaveChanges();
                    }
                }
            }
        
            Console.ReadKey();
        }
    }
}
