using CareerSearchAutomation.Core.Enums;
using log4net;
using log4net.Config;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SearchAutomation.Components;
using SearchAutomation.Core;
using SearchAutomation.Utils;

[TestFixture]
public abstract class BaseTest
{
    protected IWebDriver driver = null!;
    protected WebDriverWait wait = null!;
    protected NavbarComponent navbar = null!;
    protected CookiesComponent cookies = null!;
    
    [OneTimeSetUp]
    public void BeforeAllTests()
    {
        XmlConfigurator.Configure(new FileInfo("Log.config"));
    }

    [SetUp]
    public void Setup()
    {
        driver = WebDriverFactory.Create(BrowserType.Chrome); // Change to firefox if needed
        driver.Manage().Window.Maximize();

        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20)); // Explicit wait of 20 seconds

        navbar = new NavbarComponent(driver);
        cookies = new CookiesComponent(driver);

        driver.Navigate().GoToUrl("https://www.epam.com/");
        cookies.AcceptCookiesIfPresent();
    }

    [TearDown]
    public void Teardown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            ScreenshotMaker.TakeBrowserScreenshot(driver);
        }
        driver.Quit();
    }
}

