using OpenQA.Selenium;

namespace GlobalSearchAutomation.Locators
{
    public static class GlobalSearchLocator
    {
        public static readonly By MagnifierIcon =
            By.CssSelector(".search-icon.dark-icon.header-search__search-icon");

        public static readonly By SearchInputField =
            By.Id("new_form_search");

        public static readonly By FindButton =
            By.CssSelector(".custom-button");

        public static readonly By SearchResultLinks =
            By.CssSelector(".search-results__title a");
    }
}
