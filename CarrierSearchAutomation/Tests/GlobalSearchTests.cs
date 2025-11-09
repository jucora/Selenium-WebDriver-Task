using CarrierSearchAutomation.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace GlobalSearch.Tests
{
    [TestFixture]
    public class GlobalSearchTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = WebDriverFactory.Create("chrome");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); // Espera explícita
        }

        [TestCase("https://www.epam.com/", "BLOCKCHAIN")]
        [TestCase("https://www.epam.com/", "Cloud")]
        [TestCase("https://www.epam.com/", "Automation")]
        public void ValidateUserCanSearchBasedOnCriteria(string url, string keyword)
        {
            // Step 1: Navigate to https://www.epam.com/
            driver.Navigate().GoToUrl(url);

            // Step 2: Find a magnifier icon and click on it
            var magnifier = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.CssSelector(".search-icon.dark-icon.header-search__search-icon")
            ));
            magnifier.Click();

            // Step 3: Find a search string and put there “BLOCKCHAIN”/”Cloud”/”Automation” (use as a parameter for a test)
            var searchInput = wait.Until(ExpectedConditions.ElementIsVisible(
                By.Id("new_form_search")
            ));
            searchInput.SendKeys(keyword);

            // Step 4: Click “Find” button
            var findButton = driver.FindElement(By.CssSelector(".custom-button"));
            findButton.Click();

            // Step 5: Validate that all links in a list contain the word “BLOCKCHAIN”/”Cloud”/”Automation” in the text.
            // LINQ should be used.
            
            // Esperar hasta que aparezcan los resultados
            var links = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(
                By.CssSelector(".search-results__title a")
            ));

            // LINQ: extraer todos los textos
            var linkTexts = links.Select(link => link.Text.Trim()).ToList();

            // Mostrar los textos en la consola (útil para depuración)
            Console.WriteLine($"Links encontrados ({linkTexts.Count}):");
            linkTexts.ForEach(Console.WriteLine);

            // Validar que todos contengan la palabra clave (case-insensitive)
            bool allContainKeyword = linkTexts.All(text =>
                text.Contains(keyword, StringComparison.OrdinalIgnoreCase)
            );

            // Assertion clara
            Assert.That(allContainKeyword, Is.True,
                $"No todos los links contienen la palabra '{keyword}'.\n" +
                $"Textos: {string.Join(", ", linkTexts)}");

        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
