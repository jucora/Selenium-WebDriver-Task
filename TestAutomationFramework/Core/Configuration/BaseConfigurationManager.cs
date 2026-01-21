using Microsoft.Extensions.Configuration;

namespace TestAutomationFramework.Core.Configuration
{
    /// <summary>
    /// Clase base para manejar configuración con Singleton
    /// </summary>
    public abstract class BaseConfigurationManager
    {
        private readonly IConfigurationRoot configuration;

        protected BaseConfigurationManager(string basePath, params string[] jsonFiles)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath);

            foreach (var file in jsonFiles)
            {
                builder.AddJsonFile(file, optional: false, reloadOnChange: true);
            }

            builder.AddEnvironmentVariables();
            configuration = builder.Build();
        }

        protected string GetValue(string key, string defaultValue = "")
        {
            return configuration[key] ?? defaultValue;
        }

        protected int GetIntValue(string key, int defaultValue = 0)
        {
            return int.TryParse(configuration[key], out var value) ? value : defaultValue;
        }
    }
}
