using CarrierSearchAutomation.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace CarrierSearch.Tests
{
    [TestFixture]
    public class CarrierSearchTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = WebDriverFactory.Create("chrome");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); // Espera explícita
        }

        [TestCase("https://www.epam.com/", "Python", "All Locations")]
        [TestCase("https://www.epam.com/", "Java", "All Locations")]
        [TestCase("https://www.epam.com/", "C#", "All Locations")]
        public void ValidateUserCanSearchPositionBasedOnCriteria(string url, string keyword, string location)
        {
            // Step 1: Navigate to EPAM
            driver.Navigate().GoToUrl(url);

            // Step 2: Click "Careers" link
            var careersLink = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.CssSelector("ul:nth-child(1) > li:nth-child(5) > span:nth-child(1) > a:nth-child(1)")
            ));
            careersLink.Click();

            // Step 3: Enter programming language in Keywords field
            var keywordsField = wait.Until(ExpectedConditions.ElementIsVisible(
                By.CssSelector("#new_form_job_search-keyword")
            ));
            keywordsField.Clear();
            keywordsField.SendKeys(keyword);

            // Step 4: Select "All Locations" from dropdown
            var locationDropdown = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.CssSelector(".select2-selection__rendered")
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

            //// Step 5: Select "Remote" option
            var remoteOption = driver.FindElement(By.CssSelector("label[for='id-93414a92-598f-316d-b965-9eb0dfefa42d-remote']"));
            remoteOption.Click();

            //// Step 6: Click the "Find" button
            var findButton = driver.FindElement(By.XPath("//button[@type='submit']"));
            findButton.Click();

            // Step 7: Find the LATEST job result (last in list)
            var jobResults = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(
                By.XPath("//a[contains(text(),'View and apply')]")
            ));

            // Step 8: Click on "View and Apply"
            jobResults[^1].Click(); // último elemento de la lista

            // Step 9: Validate that keyword is present in the opened page
            bool containsKeyword = driver.PageSource.Contains(keyword, StringComparison.OrdinalIgnoreCase);
            Assert.That(containsKeyword,
                $"Expected to find '{keyword}' in the job description, but it was not found.");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
