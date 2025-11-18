using Carrier_Search_Automation.Locators;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SearchAutomation.Pages
{
    public class BasePage
    {
        protected readonly IWebDriver driver;
        protected readonly WebDriverWait wait;

        protected BasePage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        protected IReadOnlyCollection<IWebElement> WaitForAllElements(By locator) =>
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));

        protected IWebElement WaitForElement(By locator) =>
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));

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

        //
        protected void AcceptCookiesIfPresent()
        {
            try
            {
                Click(Cookies.AcceptCookiesButton);
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Cookies acceptance button not found; proceeding without accepting cookies.");
            }
        }
    }
}
