using OpenQA.Selenium;

namespace SearchAutomation.Locators.Pages
{
    public static class AboutPageLocators
    {
        public static readonly By DownloadButton =
            By.CssSelector("div[class='colctrl__holder'] span[class='button__content button__content--desktop']");
    }
}
