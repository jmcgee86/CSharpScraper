using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ScraperDb.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Text; 
using System.Drawing;
using ScraperDb.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using OpenQA.Selenium.Remote;
using System.Threading;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Firefox;


namespace ScraperDb
{
    public class GetData
    {

        public static PortfolioInfo Retrieve()
        {
           var snapshot = new PortfolioInfo();

            // ChromeOptions options = new ChromeOptions();
            // options.AddArguments("--incognito");
            // options.AddArguments("--headless");

            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService("bin/Debug/netcoreapp2.0/");

           // using (var driver = new ChromeDriver("bin/Debug/netcoreapp2.0/", options))
           using (var driver = new FirefoxDriver(service))
            {
                var keys = new Keys();
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                // driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);


                // driver.Navigate().GoToUrl("https://login.yahoo.com/config/login?.intl=us&.lang=en-US&.src=finance&.done=https%3A%2F%2Ffinance.yahoo.com%2F");
                driver.Navigate().GoToUrl("https://login.yahoo.com/?.src=finance&.intl=us&.done=https%3A%2F%2Ffinance.yahoo.com%2Fportfolios&add=1");
                // driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v2");
                // wait.Until(d => d.FindElement(By.ClassName("_299Wd")));
                // var signinButton = driver.FindElementByClassName("_299Wd");
                // signinButton.Click();
                
                var userNameField = driver.FindElementById("login-username");
                var userName = keys.Email;
                userNameField.SendKeys(userName);

                var nextButton = driver.FindElementById("login-signin");
                nextButton.Click();

                var loginAvailable = wait.Until(d => d.FindElement(By.Id("login-passwd")));

                var userPasswordField = driver.FindElementById("login-passwd");
                var password = keys.Password;
                userPasswordField.SendKeys(password);

                var loginButton = driver.FindElementById("login-signin");
                loginButton.Click();

                // var portfolioBtn = driver.FindElementByXPath("//*[@id=\"Nav-0-DesktopNav\"]/div/div[3]/div/div[1]/ul/li[2]/a");
                // portfolioBtn.Click();
                // wait.Until(d => d.FindElement(By.CssSelector("body")));
                // Console.WriteLine("found body");
                // driver.FindElement(By.CssSelector("body")).SendKeys("Keys.ESCAPE");
                // wait.Until(d => d.FindElement(By.Id("atomic")));
                // Console.WriteLine("found");
                // driver.FindElement(By.Id("atomic")).SendKeys("Keys.Escape");


                // driver.ExecuteScript("window.stop()");
                // ((IJavaScriptExecutor)driver).ExecuteScript("return window.stop();");
                // wait.Until(d => d.FindElement(By.Id("atomic")));
                // driver.ExecuteAsyncScript("window.stop()");    

                // driver.FindElement(By.CssSelector("body")).SendKeys(OpenQA.Selenium.Keys.Control + "t");
                // driver.SwitchTo().Window(driver.WindowHandles.Last());
                // driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v2");

                // try{
                //     driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v2");
                // }
                // catch(WebDriverException){
                //     Console.WriteLine("timeout");
                //     driver.ExecuteScript("window.stop();");
                //     // driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v2");
                // }
                // catch(TimeoutException){
                //     Console.WriteLine("timeout2");
                //     driver.ExecuteScript("window.stop();");
                // }
                // finally{
                //     driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v2");
                // }
   


                // try{
                //     driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v2");
                // }
                // catch(TimeoutException){
                //     // Console.WriteLine(e);
                //     driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v2");
                // }
                // finally{
                //     driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v2");
                // }


                // wait.Until(d => d.FindElement(By.Id("app")));
                // var element = driver.FindElementById("app");
                // element.SendKeys(OpenQA.Selenium.Keys.Escape);
                // Actions action = new Actions(driver);
                // action.SendKeys(OpenQA.Selenium.Keys.Escape);
//*[@id="yui_3_18_0_3_1529766166518_1004"]
                // wait.Until(d =>d.FindElement(By.XPath("//*[@id=\"yui_3_18_0_3_1529766166518_1004\"]")));
                //var financeBtn = driver.FindElementByXPath("//*[@id=\"yui_3_18_0_3_1529766166518_1004\"]");
                //financeBtn.Click();

                // Thread.Sleep(5000);
                // Console.WriteLine("sending esc");

                // driver.Keyboard.SendKeys(OpenQA.Selenium.Keys.Escape);
                // Console.WriteLine("escape key sent");

                // Thread.Sleep(5000);

                driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v2");
                                    
                wait.Until(d => d.FindElement(By.Id("__dialog")));

                var closePopup = driver.FindElement(By.XPath("//dialog[@id = '__dialog']/section/button"));
                closePopup.Click();          

                var netWorth = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/header/div/div[1]/div/div[2]/p[1]")).Text;
                string[] dayGain = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/header/div/div[1]/div/div[2]/p[2]/span")).Text.Split(" ");
                string [] totalGain = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/header/div/div[1]/div/div[2]/p[3]/span")).Text.Split(" ");
                snapshot.NetWorth = double.Parse(netWorth, NumberStyles.Currency);
                snapshot.DatePulled = DateTime.Now;
                snapshot.DayGain = double.Parse(dayGain[0]);
                snapshot.DayGainPercentage = double.Parse(dayGain[1].TrimStart(new char []{' ', '('}).TrimEnd(new char [] {'%', ' ', ')'}))/100;
                snapshot.TotalGain = double.Parse(totalGain[0]);
                snapshot.TotalGainPercentage = double.Parse(totalGain[1].TrimStart(new char []{' ', '('}).TrimEnd(new char [] {'%', ' ', ')'}))/100;

                List<StockInfo> stockDataList = new List<StockInfo>();

                // xpath of html table
                var table =	driver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table"));

                // Fetch all Rows of the table
                List<IWebElement> tableRows = new List<IWebElement>(table.FindElements(By.TagName("tr")));
                List<string> rowDataList = new List<string>();
                // Traverse each row
                foreach (var row in tableRows)
                {
                    // Fetch the columns from a particuler row
                    List<IWebElement> colsInRow = new List<IWebElement>(row.FindElements(By.TagName("td")));
                    if (colsInRow.Count > 0)
                    {
                        // Traverse each column and add to rowDataList
                        foreach (var col in colsInRow)
                        {
                            rowDataList.Add(col.Text);
                        }
                    
                    string [] colOne = rowDataList[0].ToString().Split("\n");
                    string [] colTwo = rowDataList[1].ToString().Split("\n");
                    string [] colSix = rowDataList[5].ToString().Split("\n");
                    string [] colSeven = rowDataList[6].ToString().Split("\n");
                    string [] colEight = rowDataList[7].ToString().Split(" ");

                    stockDataList.Add(new StockInfo()
                    {
                        StockSymbol = colOne[0].ToString(),
                        CurrentPrice = double.Parse(colOne[1].ToString()),
                        PriceChange = double.Parse(colTwo[1]),
                        PriceChangePercentage = double.Parse((colTwo[0]).TrimEnd(new char [] {'%', ' ', ')'}))/100,
                        Shares = double.Parse(rowDataList[2].ToString()),
                        CostBasis = double.Parse(rowDataList[3].ToString()),
                        MarketValue = double.Parse(rowDataList[4].ToString()),
                        DayGain = double.Parse(colSix[1]),
                        DayGainPercentage = double.Parse((colSix[0]).TrimEnd(new char [] {'%', ' ', ')'}))/100,
                        TotalGain = double.Parse(colSeven[1]),
                        TotalGainPercentage = double.Parse((colSeven[0]).TrimEnd(new char [] {'%', ' ', ')'}))/100,
                        Lots = int.Parse(colEight[0]),
                        Notes = rowDataList[8]
                    });
                    rowDataList.Clear();
                    }
                }
                driver.Quit();

                snapshot.StockInfo = stockDataList;
                return snapshot;
            }
        }
    }
}