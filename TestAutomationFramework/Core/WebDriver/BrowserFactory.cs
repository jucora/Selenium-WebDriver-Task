// TestAutomationFramework.Core/WebDriver/BrowserFactory.cs

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using TestAutomationFramework.Core.Enums;
using TestAutomationFramework.Core.Logging;

namespace TestAutomationFramework.Core.WebDriver
{
    /// <summary>
    /// PATRÓN FACTORY: Crea instancias de WebDriver según el tipo de navegador
    /// Encapsula la lógica de creación de drivers (SOLID - Single Responsibility)
    /// Facilita agregar nuevos navegadores sin modificar código existente (SOLID - Open/Closed)
    /// </summary>
    public class BrowserFactory : IBrowserFactory
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor con inyección de dependencias del logger
        /// </summary>
        public BrowserFactory(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Crea una instancia de WebDriver según el tipo especificado
        /// FACTORY METHOD PATTERN
        /// </summary>
        /// <param name="browserType">Tipo de navegador a crear</param>
        /// <returns>Instancia configurada de IWebDriver</returns>
        public IWebDriver CreateDriver(BrowserType browserType)
        {
            _logger.Info($"Creando instancia de WebDriver para navegador: {browserType}");

            IWebDriver driver;

            // Switch para crear el driver apropiado
            // KISS Principle: Simple y directo
            switch (browserType)
            {
                case BrowserType.Chrome:
                    driver = CreateChromeDriver();
                    break;

                case BrowserType.Firefox:
                    driver = CreateFirefoxDriver();
                    break;

                case BrowserType.Edge:
                    driver = CreateEdgeDriver();
                    break;

                default:
                    _logger.Warn($"Tipo de navegador no reconocido: {browserType}. Usando Chrome por defecto.");
                    driver = CreateChromeDriver();
                    break;
            }

            _logger.Info($"WebDriver creado exitosamente para {browserType}");
            return driver;
        }

        /// <summary>
        /// Crea y configura un driver de Chrome
        /// DRY Principle: Configuración centralizada
        /// </summary>
        private IWebDriver CreateChromeDriver()
        {
            var options = new ChromeOptions();

            // Opciones comunes de Chrome para tests
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-popup-blocking");

            // Para ejecución en servidores sin interfaz gráfica (CI/CD)
            // options.AddArgument("--headless");

            _logger.Debug("Configurando ChromeOptions con argumentos estándar");

            return new ChromeDriver(options);
        }

        /// <summary>
        /// Crea y configura un driver de Firefox
        /// </summary>
        private IWebDriver CreateFirefoxDriver()
        {
            var options = new FirefoxOptions();

            // Opciones comunes de Firefox
            options.AddArgument("--width=1920");
            options.AddArgument("--height=1080");

            _logger.Debug("Configurando FirefoxOptions con argumentos estándar");

            return new FirefoxDriver(options);
        }

        /// <summary>
        /// Crea y configura un driver de Edge
        /// </summary>
        private IWebDriver CreateEdgeDriver()
        {
            var options = new EdgeOptions();

            // Opciones comunes de Edge
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");

            _logger.Debug("Configurando EdgeOptions con argumentos estándar");

            return new EdgeDriver(options);
        }
    }
}