using OpenQA.Selenium;
using SearchAutomation.Components;
using SearchAutomation.Core;

namespace SearchAutomation.Pages
{
    public class BasePage : ElementActions
    {
        protected BasePage(IWebDriver driver) : base(driver) { }

        protected NavbarComponent Navbar =>
            new NavbarComponent(driver);
    }
}
