using Microsoft.Extensions.Configuration;

namespace TestAutomationFramework.Core.Configuration
{
    /// <summary>
    /// Singleton class that handles the framework configuration
    /// Reads configuration from appsettings.json
    /// SINGLETON PATTERN: Only one instance exists throughout the entire application
    /// </summary>
    public sealed class ConfigurationManager : IConfiguration
    {
        // Static variable that holds the single instance (Singleton)
        private static ConfigurationManager? instance;

        // Lock object for thread-safety in multithreaded environments
        private static readonly object lockObject = new object();

        // Microsoft IConfiguration used to read JSON files
        private readonly IConfigurationRoot configuration;

        /// <summary>
        /// Private constructor to prevent external instantiation (part of the Singleton pattern)
        /// </summary>
        private ConfigurationManager()
        {
            // Builds the configuration by reading the JSON files
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory + "Tests/Configuration")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{GetEnvironmentVariable()}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables(); // Allows overrides using environment variables

            configuration = builder.Build();
        }

        /// <summary>
        /// Property that provides access to the single instance (Singleton)
        /// Thread-safe using double-check locking
        /// </summary>
        public static ConfigurationManager Instance
        {
            get
            {
                // First check without lock for performance
                if (instance == null)
                {
                    // Lock to prevent multiple threads from creating instances
                    lock (lockObject)
                    {
                        // Second check inside the lock
                        if (instance == null)
                        {
                            instance = new ConfigurationManager();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Gets the environment variable from the operating system or defaults to "Production"
        /// </summary>
        private static string GetEnvironmentVariable()
        {
            return Environment.GetEnvironmentVariable("TEST_ENVIRONMENT") ?? "Production";
        }

        /// <summary>
        /// Gets the configured browser type
        /// </summary>
        public string GetBrowser()
        {
            return configuration["Browser"] ?? "Chrome";
        }

        /// <summary>
        /// Gets the configured environment (Development, Staging, Production)
        /// </summary>
        public string GetEnvironment()
        {
            return configuration["Environment"] ?? "Development";
        }

        /// <summary>
        /// Gets the base URL of the configured environment
        /// </summary>
        public string GetBaseUrl()
        {
            var environment = GetEnvironment();
            return configuration[$"Environments:{environment}:BaseUrl"] ?? string.Empty;
        }

        /// <summary>
        /// Gets the implicit wait time in seconds
        /// </summary>
        public int GetImplicitWait()
        {
            return int.Parse(configuration["Timeouts:ImplicitWait"] ?? "10");
        }

        /// <summary>
        /// Gets the explicit wait time in seconds
        /// </summary>
        public int GetExplicitWait()
        {
            return int.Parse(configuration["Timeouts:ExplicitWait"] ?? "30");
        }

        /// <summary>
        /// Gets the configured minimum logging level
        /// </summary>
        public string GetLogLevel()
        {
            return configuration["Logging:MinLevel"] ?? "Info";
        }

        /// <summary>
        /// Gets the path where screenshots will be saved
        /// </summary>
        public string GetScreenshotPath()
        {
            return configuration["ScreenshotPath"] ?? "Screenshots";
        }
    }
}