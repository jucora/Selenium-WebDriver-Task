using OpenQA.Selenium;

namespace SearchAutomation.Locators
{
    public static class GlobalSearchLocators
    {
        public static readonly By SearchInputField =
            By.Id("new_form_search");

        public static readonly By FindButton =
            By.CssSelector(".custom-button");
    }
}
