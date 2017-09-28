using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace TripPlannerPOC
{
    class TransportNswPageObjModel : BasePageObjectModel
    {
        private IWebElement From
        {
            get
            {
                return driver.FindElement(By.Id("search-input-From"));
            }
        }

        private IWebElement To { 
            get 
            {
                return driver.FindElement(By.Id("search-input-To"));
            } 
        }

        public IWebElement Go
        {
            get
            {
                return driver.FindElement(By.Id("search-button"));
            }
        }

        private ReadOnlyCollection<IWebElement> TripResults
        {
            get
            {
                try
                {
                    ReadOnlyCollection<IWebElement> Results = driver.FindElements(By.XPath("//trip-summary"));
                    return Results;
                }
                catch
                {
                    return null;
                }
            }
        }

        public TransportNswPageObjModel(IWebDriver driver) : base(driver)
        {            
        }

        private void ChooseCity(IWebElement element, string station)
        {
            element.Clear();
            element.SendKeys(station);
            //Select City from dropdown list
            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(Convert.ToInt16(ConfigurationManager.AppSettings["DefaultTimeoutSec"])));
            var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(text(),'" + station + "')]")));
            clickableElement.Click();
        }

        public void ChooseFromStation(string station)
        {
            ChooseCity(From, station);
            Console.WriteLine("Select {0} from dropdown list {1}", station, TakeScreenshot());
        }

        public void ChooseToStation(string station)
        {
            ChooseCity(To, station);
            Console.WriteLine("Select {0} from dropdown list {1}", station, TakeScreenshot());
        }

        public void ClickGo()
        {
            Console.WriteLine("Click Go button {0}", TakeScreenshot());
            Go.Click();
        }

        public int ResultsCount()
        {
            if (TripResults != null)
                return TripResults.Count;
            else
                return 0;
        }
       
    }
}
