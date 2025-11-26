using OpenQA.Selenium;

namespace SearchAutomation.Utils
{
    public static class ScreenshotMaker
    {
        public static string TakeBrowserScreenshot(IWebDriver driver)
        {
            ITakesScreenshot takesScreenshot = (ITakesScreenshot)driver;

            var now = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-fff");

            // Folder where to save the screenshots
            var screenshotsDir = Path.Combine(Environment.CurrentDirectory, "Screenshots");

            // Create folder if it does not exist
            if (!Directory.Exists(screenshotsDir))
                Directory.CreateDirectory(screenshotsDir);

            var screenshotPath = Path.Combine(screenshotsDir, $"Display_{now}.png");

            takesScreenshot.GetScreenshot().SaveAsFile(screenshotPath);

            return screenshotPath;
        }
    }
}
