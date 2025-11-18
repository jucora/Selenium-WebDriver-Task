using Carrier_Search_Automation.Locators;
using OpenQA.Selenium;
using SearchAutomation.Pages;

namespace Carrier_Search_Automation.Pages
{
    public class AboutPage : BasePage
    {
        public AboutPage(IWebDriver driver) :base(driver) { }

        public void ClickDownloadButton()
        {
            Click(AboutPageLocators.DownloadButton);
        }
    }
}
