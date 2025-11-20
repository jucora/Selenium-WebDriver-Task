using OpenQA.Selenium;

namespace SearchAutomation.Locators
{
    public static class IndexPageLocators
    {
        public static readonly By CareersLink =
            By.CssSelector("ul:nth-child(1) > li:nth-child(5) > span:nth-child(1) > a:nth-child(1)");

        public static readonly By MagnifierIcon =
            By.CssSelector(".search-icon.dark-icon.header-search__search-icon");

        public static readonly By AboutLink =
            By.XPath("//a[@class='top-navigation__item-link js-op'][normalize-space()='About']");

        public static readonly By InsightsLink =
            By.CssSelector("ul[class='top-navigation__row'] li:nth-child(3) span:nth-child(1) a:nth-child(1)");
    }
}
