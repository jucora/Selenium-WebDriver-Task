// TestAutomationFramework.Core/Utilities/ScreenshotHelper.cs

using OpenQA.Selenium;
using TestAutomationFramework.Core.Configuration;
using TestAutomationFramework.Core.Logging;

namespace TestAutomationFramework.Core.Utilities
{
    /// <summary>
    /// Clase helper para capturar screenshots cuando los tests fallan
    /// SOLID - Single Responsibility: Solo maneja capturas de pantalla
    /// </summary>
    public class ScreenshotHelper
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public ScreenshotHelper(ILogger logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Captura un screenshot con fecha y hora en el nombre
        /// Formato: TestName_yyyy-MM-dd_HH-mm-ss.png
        /// </summary>
        /// <param name="driver">Instancia de WebDriver</param>
        /// <param name="testName">Nombre del test que falló</param>
        /// <returns>Ruta completa del archivo guardado</returns>
        public string TakeScreenshot(IWebDriver driver, string testName)
        {
            try
            {
                _logger.Info($"Capturando screenshot para test: {testName}");

                // Obtiene la ruta base desde configuración
                var screenshotPath = _configuration.GetScreenshotPath();

                // Crea el directorio si no existe
                if (!Directory.Exists(screenshotPath))
                {
                    Directory.CreateDirectory(screenshotPath);
                    _logger.Debug($"Directorio de screenshots creado: {screenshotPath}");
                }

                // Genera nombre de archivo con timestamp
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                var fileName = $"{testName}_{timestamp}.png";
                var fullPath = Path.Combine(screenshotPath, fileName);

                // Captura el screenshot
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(fullPath);

                _logger.Info($"Screenshot guardado en: {fullPath}");
                return fullPath;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error al capturar screenshot para {testName}", ex);
                return null;
            }
        }

        /// <summary>
        /// Captura screenshot con mensaje personalizado
        /// </summary>
        public string TakeScreenshot(IWebDriver driver, string testName, string reason)
        {
            _logger.Info($"Capturando screenshot - Razón: {reason}");
            return TakeScreenshot(driver, testName);
        }
    }
}