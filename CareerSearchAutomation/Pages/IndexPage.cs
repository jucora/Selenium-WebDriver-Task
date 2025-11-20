using SearchAutomation.Locators;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Carrier_Search_Automation.Pages;
using Carrier_Search_Automation.Locators;

namespace SearchAutomation.Pages
{
    public class IndexPage : BasePage
    {
        public IndexPage(IWebDriver driver) : base(driver)
        {
            
        }
        public CareersPage ClickCareersLink()
        {
            Click(IndexPageLocators.CareersLink);
            return new CareersPage(driver);
        }

        public IndexPage ClickMagnifierIcon()
        {
            Click(IndexPageLocators.MagnifierIcon);
            return this;
        }

        public IndexPage EnterSearchKeyword(string keyword)
        {
            Type(GlobalSearchLocators.SearchInputField, keyword);
            return this;
        }

        public SearchPage ClickFindButton()
        {
            Click(GlobalSearchLocators.FindButton);
            return new SearchPage(driver);
        }

        public AboutPage ClickAboutLink()
        {
            Click(IndexPageLocators.AboutLink);
            return new AboutPage(driver);
        }

        public IndexPage AcceptCookies() 
        {
            AcceptCookiesIfPresent();
            return this;
        }

        public InsightsPage ClickInsightsLink()
        {
            Click(IndexPageLocators.InsightsLink);
            return new InsightsPage(driver);
        }
    }
}
