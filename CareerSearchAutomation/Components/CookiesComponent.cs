using OpenQA.Selenium;
using SearchAutomation.Core;
using SearchAutomation.Locators.Components;

namespace SearchAutomation.Components
{
    public class CookiesComponent : ElementActions
    {
        public CookiesComponent(IWebDriver driver) : base(driver)
        {
        }
    
        public void AcceptCookiesIfPresent()
        {
            try
            {
                Click(CookiesComponentLocators.AcceptCookiesButton);
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Cookies acceptance button not found; proceeding without accepting cookies.");
            }
        }
    }
}
