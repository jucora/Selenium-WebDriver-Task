using OpenQA.Selenium;
using TestAutomationFramework.Core.Configuration;
using TestAutomationFramework.Core.Logging;

namespace TestAutomationFramework.Core.Utilities
{
    /// <summary>
    /// Helper class for capturing screenshots when tests fail
    /// SOLID - Single Responsibility: Only handles screenshot capturing
    /// </summary>
    public class ScreenshotHelper
    {
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        public ScreenshotHelper(ILogger logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        /// <summary>
        /// Captures a screenshot with the date and time in the filename
        /// Format: TestName_yyyy-MM-dd_HH-mm-ss.png
        /// </summary>
        /// <param name="driver">WebDriver instance</param>
        /// <param name="testName">Name of the test that failed</param>
        /// <returns>Full path of the saved file</returns>
        public string? TakeScreenshot(IWebDriver driver, string testName)
        {
            try
            {
                logger.Info($"Capturing screenshot for test: {testName}");

                // Gets the base path from configuration
                var screenshotPath = configuration.GetScreenshotPath();

                // Creates the directory if it does not exist
                if (!Directory.Exists(screenshotPath))
                {
                    Directory.CreateDirectory(screenshotPath);
                    logger.Debug($"Screenshot directory created: {screenshotPath}");
                }

                // Genera nombre de archivo con timestamp
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                var fileName = $"{testName}_{timestamp}.png";
                var fullPath = Path.Combine(screenshotPath, fileName);

                // Captura el screenshot
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(fullPath);

                logger.Info($"Screenshot saved at: {fullPath}");

                return fullPath;
            }
            catch (Exception ex)
            {
                logger.Error($"Error capturing screenshot for {testName}", ex);
                return null;
            }
        }

        /// <summary>
        /// Captures a screenshot with a custom message
        /// </summary>
        public string TakeScreenshot(IWebDriver driver, string testName, string reason)
        {
            logger.Info($"Capturing screenshot - Reason: {reason}");
            return TakeScreenshot(driver, testName);
        }
    }
}