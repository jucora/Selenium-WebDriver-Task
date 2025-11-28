// TestAutomationFramework.Core/Configuration/ConfigurationManager.cs

using Microsoft.Extensions.Configuration;

namespace TestAutomationFramework.Core.Configuration
{
    /// <summary>
    /// Clase Singleton que maneja la configuración del framework
    /// Lee configuración desde appsettings.json
    /// PATRÓN SINGLETON: Solo existe una instancia en toda la aplicación
    /// </summary>
    public sealed class ConfigurationManager : IConfiguration
    {
        // Variable estática que contiene la única instancia (Singleton)
        private static ConfigurationManager _instance;

        // Lock object para thread-safety en ambientes multi-hilo
        private static readonly object _lock = new object();

        // IConfiguration de Microsoft para leer archivos JSON
        private readonly IConfigurationRoot _configuration;

        /// <summary>
        /// Constructor privado para evitar instanciación externa (parte del patrón Singleton)
        /// </summary>
        private ConfigurationManager()
        {
            // Construye la configuración leyendo los archivos JSON
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory + "Tests/Configuration") // NO Directory.GetCurrentDirectory()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{GetEnvironmentVariable()}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables(); // Permite override con variables de ambiente

            _configuration = builder.Build();
        }

        /// <summary>
        /// Propiedad que proporciona acceso a la única instancia (Singleton)
        /// Thread-safe usando double-check locking
        /// </summary>
        public static ConfigurationManager Instance
        {
            get
            {
                // Primer check sin lock para performance
                if (_instance == null)
                {
                    // Lock para evitar que múltiples threads creen instancias
                    lock (_lock)
                    {
                        // Segundo check dentro del lock
                        if (_instance == null)
                        {
                            _instance = new ConfigurationManager();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Obtiene la variable de ambiente del sistema operativo
        /// </summary>
        private static string GetEnvironmentVariable()
        {
            return Environment.GetEnvironmentVariable("TEST_ENVIRONMENT") ?? "Production";
        }

        /// <summary>
        /// Obtiene el tipo de navegador configurado
        /// </summary>
        public string GetBrowser()
        {
            return _configuration["Browser"] ?? "Chrome";
        }

        /// <summary>
        /// Obtiene el ambiente configurado (Development, Staging, Production)
        /// </summary>
        public string GetEnvironment()
        {
            return _configuration["Environment"] ?? "Development";
        }

        /// <summary>
        /// Obtiene la URL base del ambiente configurado
        /// </summary>
        public string GetBaseUrl()
        {
            var environment = GetEnvironment();
            return _configuration[$"Environments:{environment}:BaseUrl"];
        }

        /// <summary>
        /// Obtiene el tiempo de espera implícita en segundos
        /// </summary>
        public int GetImplicitWait()
        {
            return int.Parse(_configuration["Timeouts:ImplicitWait"] ?? "10");
        }

        /// <summary>
        /// Obtiene el tiempo de espera explícita en segundos
        /// </summary>
        public int GetExplicitWait()
        {
            return int.Parse(_configuration["Timeouts:ExplicitWait"] ?? "30");
        }

        /// <summary>
        /// Obtiene el nivel mínimo de logging configurado
        /// </summary>
        public string GetLogLevel()
        {
            return _configuration["Logging:MinLevel"] ?? "Info";
        }

        /// <summary>
        /// Obtiene la ruta donde se guardarán los screenshots
        /// </summary>
        public string GetScreenshotPath()
        {
            return _configuration["ScreenshotPath"] ?? "Screenshots";
        }
    }
}