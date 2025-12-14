using OpenQA.Selenium;
using TestAutomationFramework.Business.Pages;
using TestAutomationFramework.Core.Logging;

namespace TestAutomationFramework.Business.Components
{
    public class NavbarComponent : BasePage
    {
        private static By ServiceCategoryLink(string category) =>
            By.XPath($"//a[@class='top-navigation__sub-link' and normalize-space()='{category}']");

        private static readonly By InsightsLink =
            By.CssSelector("ul[class='top-navigation__row'] li:nth-child(3) span:nth-child(1) a:nth-child(1)");

        private static readonly By AboutLink =
            By.XPath("//a[@class='top-navigation__item-link js-op'][normalize-space()='About']");

        private static readonly By CareersLink =
            By.CssSelector("ul:nth-child(1) > li:nth-child(5) > span:nth-child(1) > a:nth-child(1)");

        private static readonly By MagnifierIcon =
            By.CssSelector(".search-icon.dark-icon.header-search__search-icon");

        private static readonly By SearchInputField =
            By.Id("new_form_search");

        private static readonly By FindButton =
            By.CssSelector(".custom-button");

        /// <summary>
        /// Constructor that calls the base constructor
        /// </summary>
        public NavbarComponent(IWebDriver driver, ILogger logger) : base(driver, logger)
        {
            Logger.Info("NavbarComponent initialized");
        }

        public CareersPage ClickCareersLink()
        {
            Click(CareersLink);
            return new CareersPage(Driver, Logger);
        }

        public void ClickServiceCategory(string category) 
        {
            Click(ServiceCategoryLink(category), $"Service link: {category}");
        }

        public NavbarComponent ClickMagnifierIcon()
        {
            Click(MagnifierIcon);
            return this;
        }

        public NavbarComponent EnterSearchKeyword(string keyword)
        {
            SendKeys(SearchInputField, keyword);
            return this;
        }

        public SearchPage ClickFindButton()
        {
            Click(FindButton);
            return new SearchPage(Driver, Logger);
        }

        public AboutPage ClickAboutLink()
        {
            Click(AboutLink);
            return new AboutPage(Driver, Logger);
        }

        public InsightsPage ClickInsightsLink()
        {
            Click(InsightsLink);
            return new InsightsPage(Driver, Logger);
        }
    }
}
