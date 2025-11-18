using BaseSearchAutomation.Helpers;
using CareerSearchAutomation.Locators;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;

namespace CareerSearchAutomation.Helpers
{
    public class CareerSearchTestHelper : BaseTest
    {
        private const string Location = "All Locations";
        protected void ClickCareersLink()
        {
            var careersLink = wait.Until(ExpectedConditions.ElementToBeClickable(
                CareerSearchLocator.CareersLink
            ));
            careersLink.Click();
        }

        protected void EnterKeyword(string keyword)
        {
            var keywordsField = wait.Until(ExpectedConditions.ElementIsVisible(
                CareerSearchLocator.KeywordsField
            ));
            keywordsField.Clear();
            keywordsField.SendKeys(keyword);
        }

        protected void SelectLocation()
        {
            CareerSearchLocator.setLocation(Location);
            var locationDropdown = wait.Until(ExpectedConditions.ElementToBeClickable(
                CareerSearchLocator.LocationDropdown
            ));

            string selectedLocation = locationDropdown.Text;

            if (!selectedLocation.Contains(Location, StringComparison.OrdinalIgnoreCase))
            {
                locationDropdown.Click();

                var locationsOption = wait.Until(ExpectedConditions.ElementToBeClickable(
                    CareerSearchLocator.LocationField
                ));
                locationsOption.Click();
            }
        }

        protected void SelectRemoteOption()
        {
            var remoteOption = driver.FindElement(CareerSearchLocator.RemoteOption);
            remoteOption.Click();
        }

        protected void ClickFindButton()
        {
            var findButton = driver.FindElement(CareerSearchLocator.FindButton);
            findButton.Click();
        }

        protected void SelectViewAndApplyFromLastResult()
        {
            var jobResults = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(
                CareerSearchLocator.JobResults
            ));

            jobResults[^1].Click();
        }

        protected void ValidateKeywordIsPresent(string keyword)
        {
            bool containsKeyword = driver.PageSource.Contains(keyword, StringComparison.OrdinalIgnoreCase);
            Assert.That(containsKeyword,
                $"Expected to find '{keyword}' in the job description, but it was not found.");
        }
    }
}
