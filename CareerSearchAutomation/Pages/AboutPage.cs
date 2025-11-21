using OpenQA.Selenium;
using SearchAutomation.Locators.Pages;

namespace SearchAutomation.Pages
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
