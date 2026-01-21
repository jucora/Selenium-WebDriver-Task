using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestAutomationFramework.Core.Configuration.Ui;
using TestAutomationFramework.Core.Logging;

namespace TestAutomationFramework.Core.Utilities
{
    /// <summary>
    /// Helper class for explicit waits
    /// DRY Principle: Centralizes reusable wait logic
    /// </summary>
    public class WaitHelper
    {
        private readonly WebDriverWait wait;
        private readonly ILogger logger;

        public WaitHelper(IWebDriver driver, ILogger logger, IConfiguration configuration)
        {
            this.logger = logger;

            var timeout = configuration.GetExplicitWait();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));

            logger.Debug($"WaitHelper initialized with a timeout of {timeout}s");

        }

        /// <summary>
        /// Waits until an element is visible
        /// </summary>
        public IWebElement WaitForElementVisible(By locator)
        {
            logger.Debug($"Waiting for the element to become visible: {locator}");

            try
            {
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                logger.Debug("Visible element found");
                return element;
            }
            catch (WebDriverTimeoutException ex)
            {
                logger.Error($"Timeout waiting for visible element: {locator}", ex);
                throw;
            }
        }

        /// <summary>
        /// Waits until an element is clickable
        /// </summary>
        public IWebElement WaitForElementClickable(By locator)
        {
            logger.Debug($"Waiting for the element to become clickable: {locator}");
            try
            {
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
                logger.Debug("Clickable element found");
                return element;
            }
            catch (WebDriverTimeoutException ex)
            {
                logger.Error($"Timeout waiting for clickable element: {locator}", ex);
                throw;
            }
        }

        /// <summary>
        /// Waits until an element exists in the DOM
        /// </summary>
        public IWebElement WaitForElementExists(By locator)
        {
            logger.Debug($"Waiting for the element to exist: {locator}");
            try
            {
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
                logger.Debug("Element found in DOM");
                return element;
            }
            catch (WebDriverTimeoutException ex)
            {
                logger.Error($"Timeout waiting for element in the DOM: {locator}", ex);
                throw;
            }
        }

        /// <summary>
        /// Waits for a custom condition
        /// </summary>
        public T WaitForCondition<T>(Func<IWebDriver, T> condition)
        {
            logger.Debug("Waiting for custom condition");
            try
            {
                return wait.Until(condition);
            }
            catch (WebDriverTimeoutException ex)
            {
                logger.Error("Timeout waiting for custom condition", ex);
                throw;
            }
        }
    }
}