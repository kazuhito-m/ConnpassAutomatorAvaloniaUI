using ConnpassAutomator.Domain.Model.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;

namespace ConnpassAutomator.Infrastructure.Datasource.Selenium
{
    public class SeleniumDatasource : ISeleniumRepository
    {
        public WebDriver CreateWebDriver(int commandTimeoutSeconds)
        {
            var driverLibDirPath = AppDomain.CurrentDomain.BaseDirectory;
            return new ChromeDriver(driverLibDirPath, new ChromeOptions(), TimeSpan.FromSeconds(commandTimeoutSeconds));
        }

        private static string ThisAssemblyDirectoryPath()
            => Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;

        public WebDriverWait CreateWait(WebDriver driver, int waitSeconds, int pollingIntervalSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitSeconds));
            wait.PollingInterval = TimeSpan.FromSeconds(pollingIntervalSeconds);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return wait;
        }
    }
}
