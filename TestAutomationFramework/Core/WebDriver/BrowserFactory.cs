using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using TestAutomationFramework.Core.Enums;
using TestAutomationFramework.Core.Logging;
using TestAutomationFramework.Core.Utilities;

namespace TestAutomationFramework.Core.WebDriver
{
    /// <summary>
    /// FACTORY PATTERN: Creates WebDriver instances based on the browser type
    /// Encapsulates driver creation logic (SOLID - Single Responsibility)
    /// Makes it easy to add new browsers without modifying existing code (SOLID - Open/Closed)
    /// </summary>
    public class BrowserFactory : IBrowserFactory
    {
        private readonly ILogger logger;

        /// <summary>
        /// Constructor with dependency injection for the logger
        /// </summary>
        public BrowserFactory(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Creates a WebDriver instance based on the specified type
        /// FACTORY METHOD PATTERN
        /// </summary>
        /// <param name="browserType">Type of browser to create</param>
        /// <returns>Configured IWebDriver instance</returns>
        public IWebDriver CreateDriver(BrowserType browserType)
        {
            logger.Info($"Creating WebDriver instance for browser: {browserType}");

            IWebDriver driver;

            // Switch to create the appropriate driver
            // KISS Principle: Simple and direct
            switch (browserType)
            {
                case BrowserType.Chrome:
                    driver = CreateChromeDriver();
                    break;

                case BrowserType.Firefox:
                    driver = CreateFirefoxDriver();
                    break;

                case BrowserType.Edge:
                    driver = CreateEdgeDriver();
                    break;

                default:
                    logger.Warn($"Unrecognized browser type: {browserType}. Using Chrome by default.");
                    driver = CreateChromeDriver();
                    break;
            }

            logger.Info($"WebDriver successfully created for {browserType}");
            return driver;
        }

        /// <summary>
        /// Creates and configures a Chrome driver
        /// DRY Principle: Centralized configuration
        /// </summary>
        //private IWebDriver CreateChromeDriver()
        //{
        //    var options = new ChromeOptions();

        //    // Common Chrome options for tests
        //    options.AddArgument("--start-maximized");
        //    options.AddArgument("--disable-notifications");
        //    options.AddArgument("--disable-popup-blocking");

        //    // Configuration for automatic downloads without prompt
        //    var downloadDir = DownloadsPath.DownloadFolder;
        //    options.AddUserProfilePreference("download.default_directory", downloadDir);
        //    options.AddUserProfilePreference("download.prompt_for_download", false);
        //    options.AddUserProfilePreference("download.directory_upgrade", true);
        //    options.AddUserProfilePreference("safebrowsing.enabled", true);

        //    // For execution on servers without a graphical interface (CI/CD)
        //    // options.AddArgument("--headless"); // Uncomment if headless mode is needed

        //    logger.Debug("Configuring ChromeOptions with standard arguments");

        //    return new ChromeDriver(options);
        //}

        private IWebDriver CreateChromeDriver()
        {
            var options = new ChromeOptions();

            bool isCI = Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true";

            logger.Info($"Running in CI: {isCI}");

            if (isCI)
            {
                // Required for GitHub Actions / Linux runners
                options.AddArgument("--headless=new");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--window-size=1920,1080");
            }
            else
            {
                // Local execution
                options.AddArgument("--start-maximized");
            }

            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-popup-blocking");

            // Downloads
            var downloadDir = DownloadsPath.DownloadFolder;
            options.AddUserProfilePreference("download.default_directory", downloadDir);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("download.directory_upgrade", true);
            options.AddUserProfilePreference("safebrowsing.enabled", true);

            logger.Debug("ChromeOptions configured successfully");

            return new ChromeDriver(options);
        }


        /// <summary>
        /// Creates and configures a Firefox driver
        /// </summary>
        private IWebDriver CreateFirefoxDriver()
        {
            var options = new FirefoxOptions();

            bool isCI = Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true";

            logger.Info($"Running Firefox in CI: {isCI}");

            if (isCI)
            {
                options.AddArgument("--headless");
                options.AddArgument("--width=1920");
                options.AddArgument("--height=1080");
            }

            // Downloads
            var downloadDir = DownloadsPath.DownloadFolder;
            options.SetPreference("browser.download.folderList", 2);
            options.SetPreference("browser.download.dir", downloadDir);
            options.SetPreference("browser.download.useDownloadDir", true);
            options.SetPreference(
                "browser.helperApps.neverAsk.saveToDisk",
                "application/pdf,application/octet-stream,application/vnd.ms-excel,application/zip"
            );
            options.SetPreference("pdfjs.disabled", true);

            logger.Debug("FirefoxOptions configured successfully");

            return new FirefoxDriver(options);
        }

        /// <summary>
        /// Creates and configures an Edge driver
        /// </summary>
        private IWebDriver CreateEdgeDriver()
        {
            var options = new EdgeOptions();

            bool isCI = Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true";

            logger.Info($"Running Edge in CI: {isCI}");

            if (isCI)
            {
                options.AddArgument("--headless=new");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--window-size=1920,1080");
            }
            else
            {
                options.AddArgument("--start-maximized");
            }

            options.AddArgument("--disable-notifications");

            logger.Debug("EdgeOptions configured successfully");

            return new EdgeDriver(options);
        }
    }
}
