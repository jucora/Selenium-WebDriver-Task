// TestAutomationFramework.Core/Utilities/WaitHelper.cs

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
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
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly ILogger _logger;

        public WaitHelper(IWebDriver driver, ILogger logger, IConfiguration configuration)
        {
            _driver = driver;
            _logger = logger;

            var timeout = configuration.GetExplicitWait();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeout));

            _logger.Debug($"WaitHelper inicializado con timeout de {timeout}s");
        }

        /// <summary>
        /// Espera hasta que un elemento sea visible
        /// </summary>
        public IWebElement WaitForElementVisible(By locator)
        {
            _logger.Debug($"Esperando a que el elemento sea visible: {locator}");
            try
            {
                var element = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                _logger.Debug("Elemento visible encontrado");
                return element;
            }
            catch (WebDriverTimeoutException ex)
            {
                _logger.Error($"Timeout esperando elemento visible: {locator}", ex);
                throw;
            }
        }

        /// <summary>
        /// Espera hasta que un elemento sea clickeable
        /// </summary>
        public IWebElement WaitForElementClickable(By locator)
        {
            _logger.Debug($"Esperando a que el elemento sea clickeable: {locator}");
            try
            {
                var element = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
                _logger.Debug("Elemento clickeable encontrado");
                return element;
            }
            catch (WebDriverTimeoutException ex)
            {
                _logger.Error($"Timeout esperando elemento clickeable: {locator}", ex);
                throw;
            }
        }

        /// <summary>
        /// Espera hasta que un elemento exista en el DOM
        /// </summary>
        public IWebElement WaitForElementExists(By locator)
        {
            _logger.Debug($"Esperando a que el elemento exista: {locator}");
            try
            {
                var element = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
                _logger.Debug("Elemento encontrado en DOM");
                return element;
            }
            catch (WebDriverTimeoutException ex)
            {
                _logger.Error($"Timeout esperando elemento en DOM: {locator}", ex);
                throw;
            }
        }

        /// <summary>
        /// Espera una condición personalizada
        /// </summary>
        public T WaitForCondition<T>(Func<IWebDriver, T> condition)
        {
            _logger.Debug("Esperando condición personalizada");
            try
            {
                return _wait.Until(condition);
            }
            catch (WebDriverTimeoutException ex)
            {
                _logger.Error("Timeout esperando condición personalizada", ex);
                throw;
            }
        }
    }
}