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

                var localOferta = driver.FindElementById("local_oferta");
                localOferta.Click();
                System.Threading.Thread.Sleep(500);
                localOferta.Click();

                System.Threading.Thread.Sleep(100);
                var locaisOferta = (new SelectElement(localOferta)).Options;
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

        public static void HabilitaLocalOferta()
        {

        }

        public static List<LocalOferta> GetLocaisOferta(IWebDriver driver)
        {
            var localOferta = driver.FindElementById("local_oferta");
            return null;
        }
    }
}
