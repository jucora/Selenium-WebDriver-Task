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
        private static DriverManager instance;

        private static readonly object lockObject = new object();

        private IWebDriver driver;
        private readonly IBrowserFactory browserFactory;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        /// <summary>
        /// Constructor privado (Singleton Pattern)
        /// Inicializa las dependencias necesarias
        /// </summary>
        private DriverManager()
        {
            logger = new Logger(nameof(DriverManager));
            configuration = ConfigurationManager.Instance;
            browserFactory = new BrowserFactory(logger);

            logger.Info("DriverManager inicializado");
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
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new DriverManager();
                        }
                    }
                }
                return instance;
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
                if (driver == null)
                {
                    var errorMsg = "WebDriver no ha sido inicializado. Llama a InitializeDriver() primero.";
                    logger.Error(errorMsg);
                    throw new InvalidOperationException(errorMsg);
                }
                return driver;
            }
        }

        /// <summary>
        /// Inicializa el WebDriver usando la configuración del framework
        /// YAGNI Principle: Solo crea el driver cuando realmente se necesita
        /// </summary>
        public void InitializeDriver()
        {
            if (driver != null)
            {
                logger.Warn("WebDriver ya estaba inicializado. Cerrando instancia anterior.");
                QuitDriver();
            }

            try
            {
                // Obtiene el tipo de navegador desde la configuración
                var browserString = configuration.GetBrowser();
                logger.Info($"Inicializando WebDriver para navegador: {browserString}");

                // Convierte el string a enum
                if (!Enum.TryParse<BrowserType>(browserString, true, out var browserType))
                {
                    logger.Warn($"Tipo de navegador '{browserString}' no válido. Usando Chrome por defecto.");
                    browserType = BrowserType.Chrome;
                }

                // Usa el Factory para crear el driver
                driver = browserFactory.CreateDriver(browserType);

                // Configura timeouts desde la configuración
                var implicitWait = configuration.GetImplicitWait();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWait);

                logger.Info($"WebDriver inicializado correctamente con timeout implícito de {implicitWait}s");
            }
            catch (Exception ex)
            {
                logger.Error("Error al inicializar WebDriver", ex);
                throw;
            }
        }

        /// <summary>
        /// Navega a la URL base configurada para el ambiente actual
        /// </summary>
        public void NavigateToBaseUrl()
        {
            var baseUrl = configuration.GetBaseUrl();
            logger.Info($"Navegando a URL base: {baseUrl}");

            Driver.Navigate().GoToUrl(baseUrl);

            logger.Info("Navegación completada exitosamente");
        }

        /// <summary>
        /// Navega a una URL específica
        /// </summary>
        public void NavigateToUrl(string url)
        {
            logger.Info($"Navegando a URL: {url}");
            Driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Cierra el navegador y libera recursos
        /// DRY Principle: Centraliza la lógica de cierre
        /// </summary>
        public void QuitDriver()
        {
            if (driver != null)
            {
                try
                {
                    logger.Info("Cerrando WebDriver...");
                    driver.Quit();
                    driver.Dispose();
                    driver = null;
                    logger.Info("WebDriver cerrado correctamente");
                }
                catch (Exception ex)
                {
                    logger.Error("Error al cerrar WebDriver", ex);
                }
            }
        }

        /// <summary>
        /// Verifica si el driver está activo
        /// </summary>
        public bool IsDriverInitialized()
        {
            return driver != null;
        }
    }
}
