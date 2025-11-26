using log4net;
using OpenQA.Selenium;
using SearchAutomation.Components;
using SearchAutomation.Core;

namespace SearchAutomation.Pages
{
    public class BasePage : ElementActions
    {
        protected ILog Log
        {
            get { return LogManager.GetLogger(this.GetType()); }
        }
        protected BasePage(IWebDriver driver) : base(driver) { }

        protected NavbarComponent Navbar =>
            new NavbarComponent(driver);
    }
}
