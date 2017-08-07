using DatabaseModel;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace ListaTodosParticipantes
{
    public class ListagemSisu
    {
        //private readonly string[] _alfabeto = "a".ToUpper().Select(x => x.ToString()).ToArray();
        private readonly string[] _alfabeto = "abcdefghijklmnopqrstuvxwyz".ToUpper().Select(x => x.ToString()).ToArray();
        private ChromeOptions options = new ChromeOptions();
        private IWebDriver driver;

        private readonly string _campoUniverdidade = "txt_ies_p";

        public ListagemSisu()
        {
            options.AddArgument("whitelisted-ips");
            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("http://sisu.mec.gov.br/selecionados");
        }

        public List<Universidade> GetUniversidades()
        {
            driver.Navigate().GoToUrl("http://sisu.mec.gov.br/selecionados");
            Thread.Sleep(1000);
            var universidades = new List<Universidade>();

            foreach (var primeiraLetra in _alfabeto.ToList())
            {
                foreach (var segundaLetra in _alfabeto.ToList())
                {
                    var textoBusca = primeiraLetra + segundaLetra;
                    SetCampoUniversidade(textoBusca);
                    universidades.AddRange(GetListaUniversidades().ToArray());
                }
            }

            return universidades.GroupBy( u => u.Nome).Select( g => g.First()).ToList();
        }

        public void GetOpcoesUniversidade(Universidade universidade)
        {
            if (SelecionarUniversidade(universidade) == false)
                return;

            HabilitaLocalOferta();

            if (universidade.LocaisOferta == null)
                universidade.LocaisOferta = new List<LocalOferta>();

            universidade.LocaisOferta.AddRange(GetLocaisOferta());

            foreach (var oferta in universidade.LocaisOferta)
            {
                SelecionaLocalOferta(oferta);


                oferta.Cursos = GetCursos() ;

                foreach (var curso in oferta.Cursos)
                {
                    SelecionaCurso(curso);
                    curso.GrausTurnos = GetGrausTurnos();
                }
            }
        }


        public void GetAprovadosUniversidade(Universidade universidade)
        {
            foreach (var oferta in universidade.LocaisOferta)
            {
                foreach (var curso in oferta.Cursos)
                {
                    foreach (var grauTurno in curso.GrausTurnos)
                    {
                        if (SelecionarUniversidade(universidade) == false)
                            continue;

                        HabilitaLocalOferta();
                        SelecionaLocalOferta(oferta);
                        SelecionaCurso(curso);
                        SelecionaGrauTurno(grauTurno);
                        ClickBuscar();

                        if(grauTurno.Aprovados ==null)
                            grauTurno.Aprovados = new List<Aprovado>();

                        grauTurno.Aprovados.AddRange(GetAprovados());
                    }
                }
            }
        }

        private IWebElement CampoUniversidade()
        {
            return driver.FindElement(By.Id(_campoUniverdidade));
        }

        private List<Universidade> GetListaUniversidades()
        {
            var liUniversidades = driver.FindElements(By.XPath("//html/body/ul/li")).ToList();
            var universidades = new List<Universidade>();

            foreach (var liUniversidade in liUniversidades)
            {
                if (liUniversidade.Text.ToUpper().Contains("NÃO FORAM ENCONTRADAS") ||
                    liUniversidade.Text.ToUpper().Contains("NAO FORAM ENCONTRADAS") ||
                    string.IsNullOrWhiteSpace(liUniversidade.Text))
                    continue;


                universidades.Add(new Universidade() { Nome = liUniversidade.Text });
            }

            return universidades;
        }


        private bool ClickPrimeiroLiUniversidade()
        {
            var liUniversidade = driver.FindElement(By.XPath("//html/body/ul/li[1]/a"));
            var text = liUniversidade.Text;

            if (string.IsNullOrWhiteSpace(text) || liUniversidade.Displayed == false)
                return false;

            liUniversidade.Click();

            return text.ToUpper().Contains("NÃO FORAM ENCONTRADAS") == false;
        }

        private void SetCampoUniversidade(string texto)
        {
            CampoUniversidade().Clear();
            Thread.Sleep(10);
            CampoUniversidade().SendKeys(texto);
            Thread.Sleep(500);
        }

        private bool SelecionarUniversidade(Universidade universidade)
        {
            var nomeBusca = universidade.Nome;

            SetCampoUniversidade(nomeBusca);

            while (nomeBusca.Length > 3 && ClickPrimeiroLiUniversidade() == false)
            {
                Thread.Sleep(200);
                nomeBusca = nomeBusca.Substring(0, nomeBusca.Length - 2);
                SetCampoUniversidade(nomeBusca);
            }

            return nomeBusca.Length > 3;
        }

        private void HabilitaLocalOferta()
        {
            var localOferta = driver.FindElement(By.Id("local_oferta"));
            localOferta.Click();
            Thread.Sleep(600);
            localOferta.Click();
        }

        private List<LocalOferta> GetLocaisOferta()
        {
            var localOferta = driver.FindElement(By.Id("local_oferta"));
            var options = (new SelectElement(localOferta)).Options.ToList();
            var locais = new List<LocalOferta>();

            foreach (var option in options)
            {
                if (option.Text.ToUpper().Contains("SELECIONE"))
                    continue;

                locais.Add(
                    new LocalOferta()
                    {
                        Nome = option.Text,
                        CodigoSisu = option.GetAttribute("value")
                    });
            }

            return locais;
        }

        private List<Curso> GetCursos()
        {
            var select = driver.FindElement(By.Id("curso_p"));
            var options = (new SelectElement(select)).Options.ToList();
            var cursos = new List<Curso>();

            foreach (var option in options)
            {
                if (option.Text.ToUpper().Contains("SELECIONE"))
                    continue;

                cursos.Add(
                    new Curso()
                    {
                        Nome = option.Text,
                        CodigoSisu = option.GetAttribute("value")
                    });
            }

            return cursos;
        }

        private List<GrauTurno> GetGrausTurnos()
        {
            var select = driver.FindElement(By.Id("grau_turno_p"));
            var options = (new SelectElement(select)).Options.ToList();
            var grausTurnos = new List<GrauTurno>();

            foreach (var option in options)
            {
                if (option.Text.ToUpper().Contains("SELECIONE"))
                    continue;

                grausTurnos.Add(
                    new GrauTurno()
                    {
                        Nome = option.Text,
                        CodigoSisu = option.GetAttribute("value"),
                    });
            }

            return grausTurnos;
        }

        private void SelecionaLocalOferta(LocalOferta oferta)
        {
            var localOferta = driver.FindElement(By.Id("local_oferta"));
            var select = new SelectElement(localOferta);
            select.SelectByValue(oferta.CodigoSisu);
            Thread.Sleep(20);
        }

        private void SelecionaCurso(Curso curso)
        {
            var cursoSelect = driver.FindElement(By.Id("curso_p"));
            var select = new SelectElement(cursoSelect);
            select.SelectByValue(curso.CodigoSisu);
            Thread.Sleep(20);
        }

        private void SelecionaGrauTurno(GrauTurno grauTurno)
        {
            var grauTurnoSelect = driver.FindElement(By.Id("grau_turno_p"));
            var select = new SelectElement(grauTurnoSelect);
            select.SelectByValue(grauTurno.CodigoSisu);
            Thread.Sleep(20);
        }

        private void ClickBuscar()
        {
            var buscar = driver.FindElement(By.ClassName("botao"));
            buscar.Click();
            Thread.Sleep(5000);
        }

        private List<Aprovado> GetAprovados()
        {
            var aprovadosDb = new List<Aprovado>();

            var listas = driver.FindElements(By.ClassName("resultado_selecionados"));

            foreach (var lista in listas)
            {
                var titulo = lista.FindElements(By.TagName("th")).First().Text;

                if (lista.FindElements(By.TagName("tr")).Any() == false)
                    continue;

                var aprovados = lista.FindElements(By.TagName("tr")).ToList();

                if (aprovados.Count < 3)
                    continue;

                aprovados.RemoveRange(0, 2);

                foreach (var aprovado in aprovados)
                {
                    var dados = aprovado.FindElements(By.TagName("td"));

                    if (dados.Any() == false || dados.Count < 4)
                        continue;

                    aprovadosDb.Add(new Aprovado()
                    {
                        Classificacao = int.Parse(dados[0].Text.Replace("º","")),
                        Nome = dados[1].Text,
                        Inscricao = dados[2].Text,
                        Nota = dados[3].Text,
                        TipoConcorrencia = titulo
                    });
                }
            }

            return aprovadosDb;
        }
    }
}
