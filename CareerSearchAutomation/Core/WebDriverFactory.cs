using CareerSearchAutomation.Core.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using SearchAutomation.Utils;

namespace SearchAutomation.Core
{
    public static class WebDriverFactory
    {
        public static IWebDriver Create(BrowserType browser = BrowserType.Chrome)
        {
            if (browser == BrowserType.Chrome)
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

            if (browser == BrowserType.Firefox)
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

                // Anti-detection preferences
                profile.SetPreference("dom.webdriver.enabled", false);
                profile.SetPreference("useAutomationExtension", false);
                profile.SetPreference("privacy.resistFingerprinting", false);
                profile.SetPreference("general.platform.override", "Win32");
                profile.SetPreference("general.buildID.override", "20100101");

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
