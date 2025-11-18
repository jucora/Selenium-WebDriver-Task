using SearchAutomation.Locators;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace SearchAutomation.Pages
{
    public class CareersPage : BasePage
    {
        private const string Location = "All Locations";

        public CareersPage(IWebDriver driver) : base(driver)
        {
        }

        public CareersPage EnterKeyword(string keyword)
        {
            Type(CareerPageLocators.KeywordsField, keyword);
            return this;
        }

        public CareersPage SelectLocation()
        {
            CareerPageLocators.setLocation(Location);
           
            string selectedLocation = GetText(CareerPageLocators.LocationDropdown);

            if (!selectedLocation.Contains(Location, StringComparison.OrdinalIgnoreCase))
            {
                Click(CareerPageLocators.LocationDropdown);
                Click(CareerPageLocators.LocationField);
            }

            return this;
        }

        public CareersPage SelectRemoteOption()
        {
            Click(CareerPageLocators.RemoteOption);
            return this;
        }

        public JobListingsPage ClickFindButton()
        {
            Click(CareerPageLocators.FindButton);
            return new JobListingsPage(driver);
        }
    }
}
