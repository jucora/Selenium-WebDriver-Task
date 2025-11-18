using OpenQA.Selenium;

namespace SearchAutomation.Locators
{
    public static class JobListingsPageLocators
    {
        public static readonly By SearchResultLinks =
            By.CssSelector(".search-results__title a");

        public static readonly By JobResults =
            By.XPath("//a[contains(text(),'View and apply')]");
    }
}
