// TestAutomationFramework.Business/PageObjects/BasePage.cs

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TestAutomationFramework.Core.Logging;
using TestAutomationFramework.Core.Utilities;
using ConfigurationManager = TestAutomationFramework.Core.Configuration.ConfigurationManager;
using IConfiguration = TestAutomationFramework.Core.Configuration.IConfiguration;

namespace TestAutomationFramework.Business.Pages
{
    /// <summary>
    /// Clase base abstracta para todos los Page Objects
    /// SOLID - Open/Closed Principle: Abierta para extensión, cerrada para modificación
    /// DRY Principle: Funcionalidad común compartida entre todas las páginas
    /// </summary>
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        protected readonly ILogger Logger;
        protected readonly WaitHelper WaitHelper;
        protected readonly IConfiguration Configuration;

        /// <summary>
        /// Constructor que inicializa las dependencias comunes
        /// </summary>
        protected BasePage(IWebDriver driver, ILogger logger)
        {
            Driver = driver;
            Logger = logger;
            Configuration = ConfigurationManager.Instance;
            WaitHelper = new WaitHelper(driver, logger, Configuration);

            Logger.Debug($"Page Object inicializado: {GetType().Name}");
        }

        #region Métodos de Interacción Comunes - DRY Principle

        /// <summary>
        /// Click en un elemento con logging automático
        /// </summary>
        protected void Click(By locator, string elementName = "")
        {
            var name = string.IsNullOrEmpty(elementName) ? locator.ToString() : elementName;
            Logger.Info($"Haciendo click en: {name}");

            try
            {
                var element = WaitHelper.WaitForElementClickable(locator);
                element.Click();
                Logger.Debug($"Click exitoso en: {name}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Error al hacer click en: {name}", ex);
                throw;
            }
        }

        /// <summary>
        /// Escribe texto en un campo con logging automático
        /// </summary>
        protected void SendKeys(By locator, string text, string elementName = "")
        {
            var name = string.IsNullOrEmpty(elementName) ? locator.ToString() : elementName;
            Logger.Info($"Escribiendo en: {name}");

            try
            {
                var element = WaitHelper.WaitForElementVisible(locator);
                element.Clear();
                element.SendKeys(text);
                Logger.Debug($"Texto ingresado exitosamente en: {name}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Error al escribir en: {name}", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene texto de un elemento
        /// </summary>
        protected string GetText(By locator, string elementName = "")
        {
            var name = string.IsNullOrEmpty(elementName) ? locator.ToString() : elementName;
            Logger.Debug($"Obteniendo texto de: {name}");

            try
            {
                var element = WaitHelper.WaitForElementVisible(locator);
                var text = element.Text;
                Logger.Debug($"Texto obtenido: '{text}' de: {name}");
                return text;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error al obtener texto de: {name}", ex);
                throw;
            }
        }

        /// <summary>
        /// Verifica si un elemento está visible
        /// </summary>
        protected bool IsElementVisible(By locator, string elementName = "")
        {
            var name = string.IsNullOrEmpty(elementName) ? locator.ToString() : elementName;
            Logger.Debug($"Verificando visibilidad de: {name}");

            try
            {
                var element = Driver.FindElement(locator);
                var isVisible = element.Displayed;
                Logger.Debug($"Elemento {name} es visible: {isVisible}");
                return isVisible;
            }
            catch (NoSuchElementException)
            {
                Logger.Debug($"Elemento {name} no encontrado");
                return false;
            }
        }

        /// <summary>
        /// Espera a que la página cargue completamente
        /// </summary>
        protected void WaitForPageLoad()
        {
            Logger.Debug("Esperando a que la página cargue completamente");

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Configuration.GetExplicitWait()));
            wait.Until(driver => ((IJavaScriptExecutor)driver)
                .ExecuteScript("return document.readyState").Equals("complete"));

            Logger.Debug("Página cargada completamente");
        }

        /// <summary>
        /// Selecciona una opción de un dropdown por texto visible
        /// </summary>
        protected void SelectDropdownByText(By locator, string text, string elementName = "")
        {
            var name = string.IsNullOrEmpty(elementName) ? locator.ToString() : elementName;
            Logger.Info($"Seleccionando '{text}' en dropdown: {name}");

            try
            {
                var element = WaitHelper.WaitForElementVisible(locator);
                var select = new SelectElement(element);
                select.SelectByText(text);
                Logger.Debug($"Opción '{text}' seleccionada en: {name}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Error al seleccionar en dropdown: {name}", ex);
                throw;
            }
        }

        /// <summary>
        /// Ejecuta JavaScript en la página
        /// </summary>
        protected object ExecuteJavaScript(string script, params object[] args)
        {
            Logger.Debug($"Ejecutando JavaScript: {script}");
            var jsExecutor = (IJavaScriptExecutor)Driver;
            return jsExecutor.ExecuteScript(script, args);
        }

        /// <summary>
        /// Hace scroll hacia un elemento
        /// </summary>
        protected void ScrollToElement(By locator, string elementName = "")
        {
            var name = string.IsNullOrEmpty(elementName) ? locator.ToString() : elementName;
            Logger.Debug($"Haciendo scroll hacia: {name}");

            var element = Driver.FindElement(locator);
            ExecuteJavaScript("arguments[0].scrollIntoView(true);", element);
        }

        #endregion

        #region Métodos de Navegación

        /// <summary>
        /// Obtiene el título de la página actual
        /// </summary>
        public string GetPageTitle()
        {
            var title = Driver.Title;
            Logger.Debug($"Título de página: {title}");
            return title;
        }

        /// <summary>
        /// Obtiene la URL actual
        /// </summary>
        public string GetCurrentUrl()
        {
            var url = Driver.Url;
            Logger.Debug($"URL actual: {url}");
            return url;
        }

        #endregion

        //
        private IReadOnlyCollection<IWebElement> WaitForAllElements(By locator)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Configuration.GetExplicitWait()));
            return wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
        }

        protected void ClickLast(By locator)
        {
            var elements = WaitForAllElements(locator);
            elements.Last().Click();
        }
    }
}