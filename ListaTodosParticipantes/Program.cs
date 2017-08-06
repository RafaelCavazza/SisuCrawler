using DatabaseModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace ListaTodosParticipantes
{
    class Program
    {
        static void Main(string[] args)
        {
            var universidades = new List<Universidade>()
            {
                new Universidade(){ Nome =  "UFBA - UNIVERSIDADE FEDERAL DA BAHIA"  }
            };

            var options = new ChromeOptions();
            options.AddArgument("whitelisted-ips");

            var driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("http://sisu.mec.gov.br/selecionados");


            foreach (var universidade in universidades)
            {
                SelecionarUniversidade(driver, universidade);
                HabilitaLocalOferta(driver);
                universidade.LocaisOferta = GetLocaisOferta(driver);

                foreach (var oferta in universidade.LocaisOferta)
                {
                    SelecionaLocalOferta(driver, oferta);
                    oferta.Cursos = GetCursos(driver);

                    foreach(var curso in oferta.Cursos)
                    {
                        
                    }
                }
            }

            Console.ReadKey();
        }

        public static void SelecionarUniversidade(IWebDriver driver, Universidade universidade)
        {
            System.Threading.Thread.Sleep(2000);
            var element = driver.FindElement(By.Id("txt_ies_p"));
            element.SendKeys(universidade.Nome);
            System.Threading.Thread.Sleep(1000);
            var liUniversidade = driver.FindElement(By.XPath("//html/body/ul/li[1]/a"));
            liUniversidade.Click();
        }

        public static void HabilitaLocalOferta(IWebDriver driver)
        {
            var localOferta = driver.FindElement(By.Id("local_oferta"));
            localOferta.Click();
            System.Threading.Thread.Sleep(500);
            localOferta.Click();
        }

        public static List<LocalOferta> GetLocaisOferta(IWebDriver driver)
        {
            var localOferta = driver.FindElement(By.Id("local_oferta"));
            var options = (new SelectElement(localOferta)).Options;
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

        public static List<Curso> GetCursos(IWebDriver driver)
        {
            var select = driver.FindElement(By.Id("curso_p"));
            var options = (new SelectElement(select)).Options;
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

        public static void SelecionaLocalOferta(IWebDriver driver, LocalOferta oferta)
        {
            var localOferta = driver.FindElement(By.Id("local_oferta"));
            var select = new SelectElement(localOferta);
            select.SelectByValue(oferta.CodigoSisu);
            System.Threading.Thread.Sleep(20);
        }
    }
}
