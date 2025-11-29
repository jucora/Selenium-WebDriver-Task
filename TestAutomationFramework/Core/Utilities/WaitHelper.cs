using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestAutomationFramework.Core.Configuration;
using TestAutomationFramework.Core.Logging;

namespace TestAutomationFramework.Core.Utilities
{
    /// <summary>
    /// Clase helper para esperas explícitas
    /// DRY Principle: Centraliza lógica de esperas reutilizables
    /// </summary>
    public class WaitHelper
    {
        private readonly WebDriverWait wait;
        private readonly ILogger logger;

        public WaitHelper(IWebDriver driver, ILogger logger, IConfiguration configuration)
        {
            this.logger = logger;

            var timeout = configuration.GetExplicitWait();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));

            logger.Debug($"WaitHelper inicializado con timeout de {timeout}s");
        }

        /// <summary>
        /// Espera hasta que un elemento sea visible
        /// </summary>
        public IWebElement WaitForElementVisible(By locator)
        {
            logger.Debug($"Esperando a que el elemento sea visible: {locator}");
            try
            {
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                logger.Debug("Elemento visible encontrado");
                return element;
            }
            catch (WebDriverTimeoutException ex)
            {
                logger.Error($"Timeout esperando elemento visible: {locator}", ex);
                throw;
            }
        }

        /// <summary>
        /// Espera hasta que un elemento sea clickeable
        /// </summary>
        public IWebElement WaitForElementClickable(By locator)
        {
            logger.Debug($"Esperando a que el elemento sea clickeable: {locator}");
            try
            {
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
                logger.Debug("Elemento clickeable encontrado");
                return element;
            }
            catch (WebDriverTimeoutException ex)
            {
                logger.Error($"Timeout esperando elemento clickeable: {locator}", ex);
                throw;
            }
        }

        /// <summary>
        /// Espera hasta que un elemento exista en el DOM
        /// </summary>
        public IWebElement WaitForElementExists(By locator)
        {
            logger.Debug($"Esperando a que el elemento exista: {locator}");
            try
            {
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
                logger.Debug("Elemento encontrado en DOM");
                return element;
            }
            catch (WebDriverTimeoutException ex)
            {
                logger.Error($"Timeout esperando elemento en DOM: {locator}", ex);
                throw;
            }
        }

        /// <summary>
        /// Espera una condición personalizada
        /// </summary>
        public T WaitForCondition<T>(Func<IWebDriver, T> condition)
        {
            logger.Debug("Esperando condición personalizada");
            try
            {
                return wait.Until(condition);
            }
            catch (WebDriverTimeoutException ex)
            {
                logger.Error("Timeout esperando condición personalizada", ex);
                throw;
            }
        }
    }
}