using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TestAutomationFramework.Core.Logging;
using TestAutomationFramework.Core.Utilities;
using ConfigurationManager = TestAutomationFramework.Core.Configuration.ConfigurationManager;
using IConfiguration = TestAutomationFramework.Core.Configuration.IConfiguration;

namespace TestAutomationFramework.Business.Pages
{
    /// <summary>
    /// Abstract base class for all Page Objects
    /// SOLID - Open/Closed Principle: Open for extension, closed for modification
    /// DRY Principle: Common functionality shared between all pages
    /// </summary>
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        protected readonly ILogger Logger;
        protected readonly WaitHelper WaitHelper;
        protected readonly IConfiguration Configuration;

        /// <summary>
        /// Constructor that initializes common dependencies
        /// </summary>
        protected BasePage(IWebDriver driver, ILogger logger)
        {
            Driver = driver;
            Logger = logger;
            Configuration = ConfigurationManager.Instance;
            WaitHelper = new WaitHelper(driver, logger, Configuration);

            Logger.Debug($"Page Object initialized: {GetType().Name}");
        }

        #region Common Interaction Methods - DRY Principle

        /// <summary>
        /// Click on an element
        /// </summary>
        protected void Click(By locator, string elementName = "")
        {
            var name = string.IsNullOrEmpty(elementName) ? locator.ToString() : elementName;
            Logger.Info($"Clicking on: {name}");

            try
            {
                var element = WaitHelper.WaitForElementClickable(locator);
                element.Click();
                Logger.Debug($"Successful click on: {name}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Error when clicking on: {name}", ex);
                throw;
            }
        }

        /// <summary>
        /// Write text in a field
        /// </summary>
        protected void SendKeys(By locator, string text, string elementName = "")
        {
            var name = string.IsNullOrEmpty(elementName) ? locator.ToString() : elementName;
            Logger.Info($"Writing in: {name}");

            try
            {
                var element = WaitHelper.WaitForElementVisible(locator);
                element.Clear();
                element.SendKeys(text);
                Logger.Debug($"Text successfully entered into: {name}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Error writing to: {name}", ex);
                throw;
            }
        }

        /// <summary>
        /// Gets text from an element
        /// </summary>
        protected string GetText(By locator, string elementName = "")
        {
            var name = string.IsNullOrEmpty(elementName) ? locator.ToString() : elementName;
            Logger.Debug($"Getting text from: {name}");

            try
            {
                var element = WaitHelper.WaitForElementVisible(locator);
                var text = element.Text;
                Logger.Debug($"Text obtained: '{text}' de: {name}");
                return text;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error getting text from: {name}", ex);
                throw;
            }
        }

        /// <summary>
        /// Check if an element is visible
        /// </summary>
        protected bool IsElementVisible(By locator, string elementName = "")
        {
            var name = string.IsNullOrEmpty(elementName) ? locator.ToString() : elementName;
            Logger.Debug($"Verifying visibility of: {name}");

            try
            {
                var element = Driver.FindElement(locator);
                var isVisible = element.Displayed;
                Logger.Debug($"Element {name} is visible: {isVisible}");
                return isVisible;
            }
            catch (NoSuchElementException)
            {
                Logger.Debug($"Element {name} not found");
                return false;
            }
        }

        /// <summary>
        /// Wait for the page to load completely
        /// </summary>
        protected void WaitForPageLoad()
        {
            Logger.Debug("Waiting for the page to load completely");

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Configuration.GetExplicitWait()));
            wait.Until(driver =>
            {
                var jsExecutor = driver as IJavaScriptExecutor;
                if (jsExecutor == null)
                    throw new InvalidOperationException("Driver does not implement IJavaScriptExecutor.");
                return jsExecutor.ExecuteScript("return document.readyState")?.Equals("complete") == true;
            });

            Logger.Debug("Page fully loaded");
        }

        /// <summary>
        /// Select an option from a dropdown by visible text
        /// </summary>
        protected void SelectDropdownByText(By locator, string text, string elementName = "")
        {
            var name = string.IsNullOrEmpty(elementName) ? locator.ToString() : elementName;
            Logger.Info($"Selecting '{text}' in dropdown: {name}");

            try
            {
                var element = WaitHelper.WaitForElementVisible(locator);
                var select = new SelectElement(element);
                select.SelectByText(text);
                Logger.Debug($"Option '{text}' selected in: {name}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Error when selecting in dropdown: {name}", ex);
                throw;
            }
        }

        /// <summary>
        /// Run JavaScript on the page
        /// </summary>
        protected object ExecuteJavaScript(string script, params object[] args)
        {
            Logger.Debug($"Running JavaScript: {script}");
            var jsExecutor = (IJavaScriptExecutor)Driver;
            return jsExecutor.ExecuteScript(script, args);
        }

        /// <summary>
        /// Scrolls to an element
        /// </summary>
        protected void ScrollToElement(By locator, string elementName = "")
        {
            var name = string.IsNullOrEmpty(elementName) ? locator.ToString() : elementName;
            Logger.Debug($"Scrolling to: {name}");

            var element = Driver.FindElement(locator);
            ExecuteJavaScript("arguments[0].scrollIntoView(true);", element);
        }

        #endregion

        #region Navigation Methods

        /// <summary>
        /// Gets the title of the current page
        /// </summary>
        public string GetPageTitle()
        {
            var title = Driver.Title;
            Logger.Debug($"Page Title: {title}");
            return title;
        }

        /// <summary>
        /// Gets the current URL
        /// </summary>
        public string GetCurrentUrl()
        {
            var url = Driver.Url;
            Logger.Debug($"Current URL: {url}");
            return url;
        }

        #endregion

        private IReadOnlyCollection<IWebElement> WaitForAllElements(By locator)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Configuration.GetExplicitWait()));
            return wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
        }

        protected void ClickLast(By locator)
        {
            var elements = WaitForAllElements(locator);
            elements.Last().Click();
        }
    }
}