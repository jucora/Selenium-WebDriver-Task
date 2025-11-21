using OpenQA.Selenium;
using SearchAutomation.Core;
using SearchAutomation.Locators.Components;
using SearchAutomation.Pages;

namespace SearchAutomation.Components
{
    public class NavbarComponent : ElementActions
    {
        public NavbarComponent(IWebDriver driver) : base(driver){}

        public CareersPage ClickCareersLink()
        {
            Click(NavbarComponentLocators.CareersLink);
            return new CareersPage(driver);
        }

        public NavbarComponent ClickMagnifierIcon()
        {
            Click(NavbarComponentLocators.MagnifierIcon);
            return this;
        }

        public NavbarComponent EnterSearchKeyword(string keyword)
        {
            Type(NavbarComponentLocators.SearchInputField, keyword);
            return this;
        }

        public SearchPage ClickFindButton()
        {
            Click(NavbarComponentLocators.FindButton);
            return new SearchPage(driver);
        }

        public AboutPage ClickAboutLink()
        {
            Click(NavbarComponentLocators.AboutLink);
            return new AboutPage(driver);
        }

        public InsightsPage ClickInsightsLink()
        {
            Click(NavbarComponentLocators.InsightsLink);
            return new InsightsPage(driver);
        }
    }
}
