using NUnit.Framework;
using OpenQA.Selenium;
using TestAutomationFramework.Core.Configuration;
using TestAutomationFramework.Core.Logging;
using TestAutomationFramework.Core.Utilities;
using TestAutomationFramework.Core.WebDriver;
using TestAutomationFramework.Business.Components;

namespace TestAutomationFramework.Tests
{
    /// <summary>
    /// Abstract base class for all tests
    /// DRY PRINCIPLE: Centralizes setup and teardown common to all tests
    /// SOLID - Open/Closed: Open for extension, closed for modification
    /// </summary>
    [TestFixture]
    public abstract class BaseTest
    {
        protected IWebDriver Driver = null!;
        protected ILogger Logger = null!;
        protected IConfiguration Configuration = null!;
        protected ScreenshotHelper ScreenshotHelper = null!;

        protected NavbarComponent navbar = null!;
        protected CookiesComponent cookies = null!;

        /// <summary>
        /// Runs ONCE before all tests in this class
        /// Ideal for configuration that does not change between tests
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Initializes configuration (Singleton)
            Configuration = ConfigurationManager.Instance;

            // Initializes logger with the test class name
            Logger = new Logger(GetType().Name);

            Logger.Info("=================================================");
            Logger.Info($"STARTING TEST SUITE: {GetType().Name}");
            Logger.Info($"Environment: {Configuration.GetEnvironment()}");
            Logger.Info($"Browser: {Configuration.GetBrowser()}");
            Logger.Info("=================================================");
        }

        /// <summary>
        /// Runs BEFORE each individual test
        /// TEMPLATE METHOD PATTERN: Defines structure that subclasses may extend
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var testName = TestContext.CurrentContext.Test.Name;
            Logger.Info($"STARTING TEST: {testName}");
            Logger.Info($"Description: {TestContext.CurrentContext.Test.FullName}");

            try
            {
                // Initializes WebDriver using Singleton + Factory
                DriverManager.Instance.InitializeDriver();
                Driver = DriverManager.Instance.Driver;

                Driver.Manage().Window.Maximize();

                // Initializes screenshot helper
                ScreenshotHelper = new ScreenshotHelper(Logger, Configuration);

                // Navigates to configured base URL
                DriverManager.Instance.NavigateToBaseUrl();

                Logger.Info($"Test '{testName}' initialized successfully");

                // Hook for subclasses (Template Method Pattern)
                AdditionalSetup();

                navbar = new NavbarComponent(Driver, Logger);

                cookies = new CookiesComponent(Driver, Logger);
                cookies.AcceptCookiesIfPresent();
            }
            catch (Exception ex)
            {
                Logger.Error($"Error during test Setup '{testName}'", ex);
                throw;
            }
        }

        /// <summary>
        /// Runs AFTER each individual test
        /// Handles screenshots in case of failure
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            var testName = TestContext.CurrentContext.Test.Name;
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;

            Logger.Info($"Test '{testName}' status: {testStatus}");

            try
            {
                // If the test failed, take screenshot with date and time
                if (testStatus == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    Logger.Error($"FAILED TEST: {testName}");
                    Logger.Error($"Message: {TestContext.CurrentContext.Result.Message}");

                    // Takes screenshot with automatic timestamp
                    var screenshotPath = ScreenshotHelper.TakeScreenshot(Driver, testName);

                    if (!string.IsNullOrEmpty(screenshotPath))
                    {
                        Logger.Info($"Screenshot saved at: {screenshotPath}");
                        // Attach screenshot to NUnit report
                        TestContext.AddTestAttachment(screenshotPath, "Failure Screenshot");
                    }
                }
                else if (testStatus == NUnit.Framework.Interfaces.TestStatus.Passed)
                {
                    Logger.Info($"SUCCESSFUL TEST: {testName}");
                }

                // Hook for subclasses
                AdditionalTearDown();
            }
            catch (Exception ex)
            {
                Logger.Error($"Error during TearDown of test '{testName}'", ex);
            }
            finally
            {
                // Always close the browser
                DriverManager.Instance.QuitDriver();
                Logger.Info($"TEST FINISHED: {testName}");
                Logger.Info("─────────────────────────────────────────────────");
            }
        }

        /// <summary>
        /// Runs ONCE after all tests in this class
        /// </summary>
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Logger.Info("=================================================");
            Logger.Info($"ENDING TEST SUITE: {GetType().Name}");
            Logger.Info("=================================================");
        }

        #region Template Methods - For subclass customization

        /// <summary>
        /// TEMPLATE METHOD: Allows child classes to add extra setup
        /// YAGNI Principle: Only implemented in subclasses that need it
        /// </summary>
        protected virtual void AdditionalSetup()
        {
            // Empty default implementation
            // Subclasses may override if they need additional setup
        }

        /// <summary>
        /// TEMPLATE METHOD: Allows child classes to add extra teardown
        /// </summary>
        protected virtual void AdditionalTearDown()
        {
            // Empty default implementation
            // Subclasses may override if they need additional teardown
        }

        #endregion

        #region Helper Methods for Tests

        /// <summary>
        /// Helper to manually take a screenshot at any point in the test
        /// </summary>
        protected void TakeScreenshot(string reason)
        {
            var testName = TestContext.CurrentContext.Test.Name;
            ScreenshotHelper.TakeScreenshot(Driver, testName, reason);
        }

        #endregion
    }
}
