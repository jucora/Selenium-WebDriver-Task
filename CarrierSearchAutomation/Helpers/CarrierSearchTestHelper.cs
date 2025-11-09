using Carrier_Search_Automation.Locators;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace Carrier_Search_Automation.Helpers
{
    public class CarrierSearchTestHelper : Search
    {
        protected void clickCarriersLink()
        {
            var careersLink = wait.Until(ExpectedConditions.ElementToBeClickable(
                CarrierSearchLocator.CarriersLink
            ));
            careersLink.Click();
        }

        protected void enterKeyword(string keyword)
        {
            var keywordsField = wait.Until(ExpectedConditions.ElementIsVisible(
                CarrierSearchLocator.KeywordsField
            ));
            keywordsField.Clear();
            keywordsField.SendKeys(keyword);
        }

        protected void selectLocation(string location)
        {
            var locationDropdown = wait.Until(ExpectedConditions.ElementToBeClickable(
                CarrierSearchLocator.LocationDropdown
            ));

            string selectedLocation = locationDropdown.Text;

            if (!selectedLocation.Contains(location, StringComparison.OrdinalIgnoreCase))
            {
                locationDropdown.Click();

                var allLocationsOption = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath($"//li[contains(@class, 'select2-results__option') and normalize-space(text())='{location}']")
                ));
                allLocationsOption.Click();
            }
        }

        protected void selectRemoteOption()
        {
            var remoteOption = driver.FindElement(CarrierSearchLocator.RemoteOption);
            remoteOption.Click();
        }

        protected void clickFindButton()
        {
            var findButton = driver.FindElement(CarrierSearchLocator.FindButton);
            findButton.Click();
        }

        protected void selectViewAndApplyFromLastResult()
        {
            var jobResults = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(
                CarrierSearchLocator.JobResults
            ));

            jobResults[^1].Click();
        }

        protected void validateKeywordIsPresent(string keyword)
        {
            bool containsKeyword = driver.PageSource.Contains(keyword, StringComparison.OrdinalIgnoreCase);
            Assert.That(containsKeyword,
                $"Expected to find '{keyword}' in the job description, but it was not found.");
        }
    }
}
