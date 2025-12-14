using Reqnroll;
using Reqnroll.BoDi;
using TestAutomationFramework.Business.Components;
using TestAutomationFramework.Core.Configuration;
using TestAutomationFramework.Core.Logging;
using TestAutomationFramework.Core.Utilities;
using TestAutomationFramework.Core.WebDriver;

[Binding]
public class ReqnrollDriverHooks
{
    private readonly IObjectContainer container;

    public ReqnrollDriverHooks(IObjectContainer container)
    {
        this.container = container;
    }

    [BeforeScenario]
    public void Setup()
    {
        var logger = new Logger("ReqnrollTest");

        DriverManager.Instance.InitializeDriver();
        var driver = DriverManager.Instance.Driver;

        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl(ConfigurationManager.Instance.GetBaseUrl());

        var cookies = new CookiesComponent(driver, logger);
        cookies.AcceptCookiesIfPresent();

        var context = new UiTestContext
        {
            Driver = driver,
            Logger = logger,
            Navbar = new NavbarComponent(driver, logger),
            WaitHelper = new WaitHelper(driver, logger, ConfigurationManager.Instance)
        };

        container.RegisterInstanceAs(context);
    }

    [AfterScenario]
    public void TearDown()
    {
        DriverManager.Instance.QuitDriver();
    }
}
