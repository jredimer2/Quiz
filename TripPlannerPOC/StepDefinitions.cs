using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Chrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TripPlannerPOC
{

    public class SourceDestinationInfo
    {
        public string Source { get; set; }
        public string Destination { get; set; }
    }

    [Binding]
    public class StepDefinitions
    {
        private IWebDriver driver { get; set; }
        private TransportNswPageObjModel TransportNswPageObjModel { get; set; }

        public StepDefinitions()
        {
            driver = null;
        }

        private void InitPageObjectModels(IWebDriver driver)
        {
            TransportNswPageObjModel = new TransportNswPageObjModel(this.driver);
        }

        [BeforeScenario]
        private void LaunchBrowser()
        {
            try
            {
                switch (ConfigurationManager.AppSettings["browser"])
                {
                    case "Chrome":
                        this.driver = new ChromeDriver();
                        InitPageObjectModels(this.driver);
                        break;

                    default:
                        throw new Exception("[" + ConfigurationManager.AppSettings["browser"] + "] browser not currently supported");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception LaunchBrowser() - " + e.Message);
                throw;
            }
        }

        // Commenting out below decorator for demo purposes. Uncomment below decorator if you wish to close browser after each test case Scenario.

        //[AfterScenario]
        private void PerformCleanup() {
            if (this.driver != null)
                this.driver.Quit();
        }


        [Given(@"Phileas is planning a trip")]
        public void GivenPhileasIsPlanningATrip()
        {
            try
            {
                if (this.driver != null)
                {
                    Console.WriteLine("GoinG to URL {0}", ConfigurationManager.AppSettings["URL"]);
                    this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60); // Temporarily allow 60 mins for page to load
                    this.driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["URL"]);
                }
            }
            catch
            {
                Console.WriteLine("Exception while going to URL {0}", ConfigurationManager.AppSettings["URL"]);
                throw;
            }
            finally
            {
                this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Convert.ToInt16(ConfigurationManager.AppSettings["DefaultTimeoutSec"]));
            }
        }

        [When(@"he executes a trip plan from '(.*)' to '(.*)'")]
        public void WhenHeExecutesATripPlanFromTo(string from, string to)
        {
            TransportNswPageObjModel.ChooseFromStation(from);
            TransportNswPageObjModel.ChooseToStation(to);
            TransportNswPageObjModel.ClickGo();
        }

        [Then(@"a list of steps should be provided")]
        public void ThenAListOfStepsShouldBeProvided()
        {
            TransportNswPageObjModel.AssertCount(0, TransportNswPageObjModel.ResultsCount(), "Verify number of results is greater than 0");
            TransportNswPageObjModel.TakeScreenshot();
        }

    }
}
