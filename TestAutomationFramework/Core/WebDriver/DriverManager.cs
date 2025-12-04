using OpenQA.Selenium;
using TestAutomationFramework.Core.Configuration;
using TestAutomationFramework.Core.Enums;
using TestAutomationFramework.Core.Logging;

namespace TestAutomationFramework.Core.WebDriver
{
    /// <summary>
    /// SINGLETON PATTERN: Manages a single WebDriver instance per execution thread
    /// Thread-safe using ThreadLocal for parallel test execution
    /// SOLID - Single Responsibility: Only manages the driver lifecycle
    /// </summary>
    public sealed class DriverManager
    {
        // ThreadLocal allows each thread to have its own driver instance
        // This is crucial for parallel test execution
        [ThreadStatic]
        private static DriverManager instance;

        private static readonly object lockObject = new object();

        private IWebDriver driver;
        private readonly IBrowserFactory browserFactory;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        /// <summary>
        /// Private constructor (Singleton Pattern)
        /// Initializes required dependencies
        /// </summary>
        private DriverManager()
        {
            logger = new Logger(nameof(DriverManager));
            configuration = ConfigurationManager.Instance;
            browserFactory = new BrowserFactory(logger);

            logger.Info("DriverManager initialized");
        }

        /// <summary>
        /// Property providing access to the single instance (Singleton)
        /// Thread-safe for parallel test execution
        /// </summary>
        public static DriverManager Instance
        {
            get
            {
                // ThreadStatic ensures each thread gets its own instance
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new DriverManager();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Property to access the current WebDriver
        /// Throws an exception if the driver has not been initialized
        /// </summary>
        public IWebDriver Driver
        {
            get
            {
                if (driver == null)
                {
                    var errorMsg = "WebDriver has not been initialized. Call InitializeDriver() first.";
                    logger.Error(errorMsg);
                    throw new InvalidOperationException(errorMsg);
                }
                return driver;
            }
        }

        /// <summary>
        /// Initializes the WebDriver using the framework configuration
        /// YAGNI Principle: Creates the driver only when needed
        /// </summary>
        public void InitializeDriver()
        {
            if (driver != null)
            {
                logger.Warn("WebDriver was already initialized. Closing previous instance.");
                QuitDriver();
            }

            try
            {
                // Gets the browser type from configuration
                var browserString = configuration.GetBrowser();
                logger.Info($"Initializing WebDriver for browser: {browserString}");

                // Converts string to enum
                if (!Enum.TryParse<BrowserType>(browserString, true, out var browserType))
                {
                    logger.Warn($"Browser type '{browserString}' is not valid. Using Chrome as default.");
                    browserType = BrowserType.Chrome;
                }

                // Uses the Factory to create the driver
                driver = browserFactory.CreateDriver(browserType);

                // Configures timeouts from configuration
                var implicitWait = configuration.GetImplicitWait();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWait);

                logger.Info($"WebDriver successfully initialized with implicit timeout of {implicitWait}s");
            }
            catch (Exception ex)
            {
                logger.Error("Error initializing WebDriver", ex);
                throw;
            }
        }

        /// <summary>
        /// Navigates to the base URL configured for the current environment
        /// </summary>
        public void NavigateToBaseUrl()
        {
            var baseUrl = configuration.GetBaseUrl();
            logger.Info($"Navigating to base URL: {baseUrl}");

            Driver.Navigate().GoToUrl(baseUrl);

            logger.Info("Navigation completed successfully");
        }

        /// <summary>
        /// Navigates to a specific URL
        /// </summary>
        public void NavigateToUrl(string url)
        {
            logger.Info($"Navigating to URL: {url}");
            Driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Closes the browser and releases resources
        /// DRY Principle: Centralizes shutdown logic
        /// </summary>
        public void QuitDriver()
        {
            if (driver != null)
            {
                try
                {
                    logger.Info("Closing WebDriver...");
                    driver.Quit();
                    driver.Dispose();
                    driver = null;
                    logger.Info("WebDriver closed successfully");
                }
                catch (Exception ex)
                {
                    logger.Error("Error closing WebDriver", ex);
                }
            }
        }

        /// <summary>
        /// Checks whether the driver is initialized
        /// </summary>
        public bool IsDriverInitialized()
        {
            return driver != null;
        }
    }
}
