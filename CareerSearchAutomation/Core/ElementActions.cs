using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SearchAutomation.Base;
using SeleniumExtras.WaitHelpers;

namespace SearchAutomation.Core
{
    public class ElementActions : BaseTest
    {
        protected ElementActions(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        protected IReadOnlyCollection<IWebElement> WaitForAllElements(By locator) =>
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));

        protected IWebElement WaitForElement(By locator) =>
            wait.Until(ExpectedConditions.ElementToBeClickable(locator));

        protected void ClickLast(By locator) 
        {
            var elements = WaitForAllElements(locator);
            elements.Last().Click();
        }

        protected void Click(By locator) =>
            WaitForElement(locator).Click();

        protected void Type(By locator, string text)
        {
            var element = WaitForElement(locator);
            element.Clear();
            element.SendKeys(text);
        }

        protected string GetText(By locator) =>
            WaitForElement(locator).Text;
    }
}
