using Microsoft.Extensions.Configuration;

namespace TestAutomationFramework.Core.Configuration.Api
{
    /// <summary>
    /// Singleton class that handles the framework configuration
    /// Reads configuration from appsettings.json
    /// SINGLETON PATTERN: Only one instance exists throughout the entire application
    /// </summary>
    public sealed class ApiConfigurationManager : IApiConfiguration
    {
        private static ApiConfigurationManager? instance;

        private static readonly object lockObject = new object();

        private readonly IConfigurationRoot configuration;

        /// <summary>
        /// Private constructor to prevent external instantiation (part of the Singleton pattern)
        /// </summary>
        private ApiConfigurationManager()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory + "Tests/Configuration/Api")
                .AddJsonFile("apisettings.json", optional: false, reloadOnChange: true);

            configuration = builder.Build();
        }

        /// <summary>
        /// Property that provides access to the single instance (Singleton)
        /// Thread-safe using double-check locking
        /// </summary>
        public static ApiConfigurationManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new ApiConfigurationManager();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Gets the base URL of the configured environment
        /// </summary>
        public string GetBaseUrl()
        {
            return configuration["BaseUrl"] ?? string.Empty;
        }

        public string GetTestUser()
        {
            return configuration["NewUser"] ?? string.Empty;
        }
    }
}