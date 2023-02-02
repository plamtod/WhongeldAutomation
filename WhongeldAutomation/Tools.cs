namespace WhongeldAutomation
{
    using System;
    using System.Collections.Generic;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;

    internal class Tools
    {
        private static IWebDriver? driver;

        public Tools()
        {
            var options = new ChromeOptions();
            options.AddArguments(new List<string>() { "disable-gpu" });
            //Opens Chrome and accept all the SSL certificates        
            options.AddArguments("ignore-certificate-errors");

            driver = new ChromeDriver(options);
        }

        Dictionary<string, Action<Tuple<string, string>>> commands =
              new Dictionary<string, Action<Tuple<string, string>>> {
                { "nav", t => { Navigate(t.Item1); } },
                { "cbt", t => { ClickByText(t.Item1); } },
                { "cbp", t => { ClickByXPath(t.Item1); } },
                { "cbc", t => { ClickByCssSeclector(t.Item1); } },
                { "si",  t => { SetInput(t.Item1, t.Item2); } },
                { "si2",  t => { SetInput2(t.Item1, t.Item2); } },
                { "clk", t => { Click(t.Item1); } },
                { "sdd", t => { SetDD(t.Item1, t.Item2); } },
                { "up", t => { UploadFile(t.Item1, t.Item2); } },
          };

        public void Execute(dynamic[] dic)
        {

            foreach (var item in dic)
            {
                commands[item.Action].Invoke(item.Args);
            }
        }

        static void SetDD(string id, string value)
        {
            var dropdown = ((new WebDriverWait(driver, TimeSpan.FromSeconds(5))
             .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                By.Id(id))
             )));
            SelectElement selectTag = new SelectElement(dropdown);
            selectTag.SelectByValue(value);
        }

        static void SetInput(string id, string value)
        {

            var input = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
           .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(id)));
            input.SendKeys(value);


        }

        static void SetInput2(string path, string value)
        {
            var input = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
           .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(path)));
            input.Click();
            Task.Delay(500).Wait();
        }


        static void Click(string id)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
               .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(id)))
               .Click();
        }

        static void ClickByText(string label)
        {
            driver?.FindElement(By.LinkText(label)).Click();
        }

        static void ClickByXPath(string path)
        {
            driver?.FindElement(By.XPath(path)).Click();
        }


        static void ClickByCssSeclector(string css)
        {
            driver?.FindElement(By.CssSelector(css)).Click();
        }

        static void Navigate(string url)
        {
            driver?.Navigate().GoToUrl(url);
        }

        static void UploadFile(string upload, string file)
        {
            var upload_file = driver.FindElement(By.Id(upload));

            upload_file.SendKeys(file);
            Task.Delay(1500).Wait();
        }


    }
}
