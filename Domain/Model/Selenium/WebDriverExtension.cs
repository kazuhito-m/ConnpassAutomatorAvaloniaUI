using OpenQA.Selenium;

namespace ConnpassAutomator.Domain.Model.Selenium
{
    public static class WebDriverExtension
    {
        public static void InputText(this WebDriver driver, string elementName, string inputText)
            => driver.FindElement(By.Name(elementName)).SendKeys(inputText);
    }
}
