// TestAutomationFramework.Core/WebDriver/DriverManager.cs

using OpenQA.Selenium;
using TestAutomationFramework.Core.Configuration;
using TestAutomationFramework.Core.Enums;
using TestAutomationFramework.Core.Logging;

namespace TestAutomationFramework.Core.WebDriver
{
    /// <summary>
    /// PATRÓN SINGLETON: Gestiona una única instancia de WebDriver por hilo de ejecución
    /// Thread-safe usando ThreadLocal para ejecución paralela de tests
    /// SOLID - Single Responsibility: Solo se encarga de gestionar el ciclo de vida del driver
    /// </summary>
    public sealed class DriverManager
    {
        // ThreadLocal permite que cada hilo tenga su propia instancia del driver
        // Esto es crucial para ejecutar tests en paralelo
        [ThreadStatic]
        private static DriverManager _instance;

        private static readonly object _lock = new object();

        private IWebDriver _driver;
        private readonly IBrowserFactory _browserFactory;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor privado (Singleton Pattern)
        /// Inicializa las dependencias necesarias
        /// </summary>
        private DriverManager()
        {
            _logger = new Logger(nameof(DriverManager));
            _configuration = ConfigurationManager.Instance;
            _browserFactory = new BrowserFactory(_logger);

            _logger.Info("DriverManager inicializado");
        }

        /// <summary>
        /// Propiedad que proporciona acceso a la única instancia (Singleton)
        /// Thread-safe para ejecución paralela de tests
        /// </summary>
        public static DriverManager Instance
        {
            get
            {
                // ThreadStatic hace que cada thread tenga su propia instancia
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DriverManager();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Propiedad para acceder al WebDriver actual
        /// Lanza excepción si el driver no ha sido inicializado
        /// </summary>
        public IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    var errorMsg = "WebDriver no ha sido inicializado. Llama a InitializeDriver() primero.";
                    _logger.Error(errorMsg);
                    throw new InvalidOperationException(errorMsg);
                }
                return _driver;
            }
        }

        /// <summary>
        /// Inicializa el WebDriver usando la configuración del framework
        /// YAGNI Principle: Solo crea el driver cuando realmente se necesita
        /// </summary>
        public void InitializeDriver()
        {
            if (_driver != null)
            {
                _logger.Warn("WebDriver ya estaba inicializado. Cerrando instancia anterior.");
                QuitDriver();
            }

            try
            {
                // Obtiene el tipo de navegador desde la configuración
                var browserString = _configuration.GetBrowser();
                _logger.Info($"Inicializando WebDriver para navegador: {browserString}");

                // Convierte el string a enum
                if (!Enum.TryParse<BrowserType>(browserString, true, out var browserType))
                {
                    _logger.Warn($"Tipo de navegador '{browserString}' no válido. Usando Chrome por defecto.");
                    browserType = BrowserType.Chrome;
                }

                // Usa el Factory para crear el driver
                _driver = _browserFactory.CreateDriver(browserType);

                // Configura timeouts desde la configuración
                var implicitWait = _configuration.GetImplicitWait();
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWait);

                _logger.Info($"WebDriver inicializado correctamente con timeout implícito de {implicitWait}s");
            }
            catch (Exception ex)
            {
                _logger.Error("Error al inicializar WebDriver", ex);
                throw;
            }
        }

        /// <summary>
        /// Navega a la URL base configurada para el ambiente actual
        /// </summary>
        public void NavigateToBaseUrl()
        {
            var baseUrl = _configuration.GetBaseUrl();
            _logger.Info($"Navegando a URL base: {baseUrl}");

            Driver.Navigate().GoToUrl(baseUrl);

            _logger.Info("Navegación completada exitosamente");
        }

        /// <summary>
        /// Navega a una URL específica
        /// </summary>
        public void NavigateToUrl(string url)
        {
            _logger.Info($"Navegando a URL: {url}");
            Driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Cierra el navegador y libera recursos
        /// DRY Principle: Centraliza la lógica de cierre
        /// </summary>
        public void QuitDriver()
        {
            if (_driver != null)
            {
                try
                {
                    _logger.Info("Cerrando WebDriver...");
                    _driver.Quit();
                    _driver.Dispose();
                    _driver = null;
                    _logger.Info("WebDriver cerrado correctamente");
                }
                catch (Exception ex)
                {
                    _logger.Error("Error al cerrar WebDriver", ex);
                }
            }
        }

        /// <summary>
        /// Verifica si el driver está activo
        /// </summary>
        public bool IsDriverInitialized()
        {
            return _driver != null;
        }
    }
}
