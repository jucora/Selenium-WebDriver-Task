using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using SearchAutomation.Utils;

namespace SearchAutomation.Drivers
{
    public static class WebDriverFactory
    {
        public static IWebDriver Create(string browser = "firefox")
        {
            if (browser.ToLower() == "chrome")
            {
                var downloadDir = ProjectPaths.DownloadFolder;
                Directory.CreateDirectory(downloadDir);

                var options = new ChromeOptions();
                options.AddUserProfilePreference("download.default_directory", downloadDir);
                options.AddUserProfilePreference("download.prompt_for_download", false);
                options.AddUserProfilePreference("download.directory_upgrade", true);
                options.AddUserProfilePreference("safebrowsing.enabled", true);

                return new ChromeDriver(options);
            }

            if (browser.ToLower() == "firefox")
            {
                var downloadDir = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Downloads"
                );

                Directory.CreateDirectory(downloadDir);

                var profile = new FirefoxProfile();
                profile.SetPreference("browser.download.folderList", 2);
                profile.SetPreference("browser.download.dir", downloadDir);
                profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/pdf");

                var options = new FirefoxOptions
                {
                    Profile = profile
                };

                return new FirefoxDriver(options);
            }

            throw new ArgumentException($"Browser not supported: {browser}");
        }
    }
}
