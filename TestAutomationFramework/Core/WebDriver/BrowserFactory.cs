using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using TestAutomationFramework.Core.Enums;
using TestAutomationFramework.Core.Logging;
using TestAutomationFramework.Core.Utilities;

namespace TestAutomationFramework.Core.WebDriver
{
    /// <summary>
    /// PATRÓN FACTORY: Crea instancias de WebDriver según el tipo de navegador
    /// Encapsula la lógica de creación de drivers (SOLID - Single Responsibility)
    /// Facilita agregar nuevos navegadores sin modificar código existente (SOLID - Open/Closed)
    /// </summary>
    public class BrowserFactory : IBrowserFactory
    {
        private readonly ILogger logger;

        /// <summary>
        /// Constructor con inyección de dependencias del logger
        /// </summary>
        public BrowserFactory(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Crea una instancia de WebDriver según el tipo especificado
        /// FACTORY METHOD PATTERN
        /// </summary>
        /// <param name="browserType">Tipo de navegador a crear</param>
        /// <returns>Instancia configurada de IWebDriver</returns>
        public IWebDriver CreateDriver(BrowserType browserType)
        {
            logger.Info($"Creando instancia de WebDriver para navegador: {browserType}");

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
                    logger.Warn($"Tipo de navegador no reconocido: {browserType}. Usando Chrome por defecto.");
                    driver = CreateChromeDriver();
                    break;
            }

            logger.Info($"WebDriver creado exitosamente para {browserType}");
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

            // Configuración para descargas automáticas sin prompt
            var downloadDir = DownloadsPath.DownloadFolder;
            options.AddUserProfilePreference("download.default_directory", downloadDir);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("download.directory_upgrade", true);
            options.AddUserProfilePreference("safebrowsing.enabled", true);

            // Para ejecución en servidores sin interfaz gráfica (CI/CD)
            // options.AddArgument("--headless");

            logger.Debug("Configurando ChromeOptions con argumentos estándar");

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

            // Configuración para descargas automáticas sin prompt
            var downloadDir = DownloadsPath.DownloadFolder;
            options.SetPreference("browser.download.folderList", 2);
            options.SetPreference("browser.download.dir", downloadDir);
            options.SetPreference("browser.download.useDownloadDir", true);

            // Evitar popup de "guardar como"
            options.SetPreference("browser.helperApps.neverAsk.saveToDisk",
                "application/pdf,application/octet-stream,application/vnd.ms-excel,application/zip");

            // Desactivar visor interno de PDF para que los descargue
            options.SetPreference("pdfjs.disabled", true);

            logger.Debug("Configurando FirefoxOptions con argumentos estándar");

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

            logger.Debug("Configurando EdgeOptions con argumentos estándar");

            return new EdgeDriver(options);
        }
    }
}