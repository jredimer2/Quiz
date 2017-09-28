using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TripPlannerPOC
{
    class BasePageObjectModel
    {
        public IWebDriver driver { get; set; }

        public BasePageObjectModel(IWebDriver driver0)
        {
            driver = driver0;
        }

        public void AssertCount(int expected, int actual, string message)
        {
            try
            {
                Assert.IsTrue(actual > expected);
                Console.WriteLine(message + " Actual [" + actual + "] > Expected [" + expected + "] - PASS " + TakeScreenshot());
            }
            catch
            {
                Console.WriteLine(message + " Actual [" + actual + "] > Expected [" + expected + "] - FAIL " + TakeScreenshot());
                throw;
            }
        }


        public Uri TakeScreenshot()
        {
            try
            {
                string fileNameBase = string.Format("screenshot_{0}", DateTime.Now.ToString("HHmmssfff"));

                var artifactDirectory = Path.Combine(Directory.GetCurrentDirectory(), "screenshots");
                if (!Directory.Exists(artifactDirectory))
                    Directory.CreateDirectory(artifactDirectory);

                ITakesScreenshot takesScreenshot = driver as ITakesScreenshot;

                if (takesScreenshot != null)
                {
                    var screenshot = takesScreenshot.GetScreenshot();

                    string screenshotFilePath = Path.Combine(artifactDirectory, fileNameBase + ".jpg");

                    screenshot.SaveAsFile(screenshotFilePath, OpenQA.Selenium.ScreenshotImageFormat.Jpeg);

                    return new Uri(screenshotFilePath);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while taking screenshot: {0}", e);
                throw;
            }
        }

    }
}
