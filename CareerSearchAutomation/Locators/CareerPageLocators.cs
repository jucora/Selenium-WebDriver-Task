using OpenQA.Selenium;

namespace SearchAutomation.Locators
{
    public static class CareerPageLocators
    {
        private static string selectedLocation = string.Empty;

        public static readonly By KeywordsField = 
            By.Id("new_form_job_search-keyword");

        public static readonly By LocationField =
            By.XPath($"//li[contains(@class, 'select2-results__option') and normalize-space(text())='{selectedLocation}']");

        public static readonly By LocationDropdown = 
            By.CssSelector(".select2-selection__rendered");

        public static readonly By RemoteOption =
            By.CssSelector("label[for='id-93414a92-598f-316d-b965-9eb0dfefa42d-remote']");

        public static readonly By FindButton = 
            By.CssSelector("button[type='submit']");

        public static void setLocation(string location) 
        {
            selectedLocation = location;
        }
    }
}
