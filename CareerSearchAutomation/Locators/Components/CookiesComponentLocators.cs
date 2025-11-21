using OpenQA.Selenium;

namespace SearchAutomation.Locators.Components
{
    public static class CookiesComponentLocators
    {
        public static readonly By AcceptCookiesButton = 
            By.CssSelector("button[id=onetrust-accept-btn-handler]");
    }
}
