using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ConnpassAutomator.Domain.Model.Selenium
{
    public interface ISeleniumRepository
    {
        WebDriver CreateWebDriver(int commandTimeoutSeconds);
        WebDriverWait CreateWait(WebDriver driver, int waitSeconds, int pollingIntervalSeconds);
    }
}
