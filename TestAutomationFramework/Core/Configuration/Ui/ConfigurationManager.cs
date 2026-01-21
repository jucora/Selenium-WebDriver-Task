namespace TestAutomationFramework.Core.Configuration
{
    public sealed class ConfigurationManager : BaseConfigurationManager, IConfiguration
    {
        private static readonly Lazy<ConfigurationManager> instance =
            new(() => new ConfigurationManager());

        public static ConfigurationManager Instance => instance.Value;

        private ConfigurationManager()
            : base(AppContext.BaseDirectory + "Tests/Configuration",
                   "appsettings.json",
                   $"appsettings.{GetEnvironmentVariable()}.json")
        { }

        private static string GetEnvironmentVariable()
        {
            return Environment.GetEnvironmentVariable("TEST_ENVIRONMENT") ?? "Production";
        }

        public string GetBrowser() => GetValue("Browser", "Chrome");
        public string GetEnvironment() => GetValue("Environment", "Development");
        public string GetBaseUrl() => GetValue($"Environments:{GetEnvironment()}:BaseUrl");
        public int GetImplicitWait() => GetIntValue("Timeouts:ImplicitWait", 10);
        public int GetExplicitWait() => GetIntValue("Timeouts:ExplicitWait", 30);
        public string GetLogLevel() => GetValue("Logging:MinLevel", "Info");
        public string GetScreenshotPath() => GetValue("ScreenshotPath", "Screenshots");
    }
}
