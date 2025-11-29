using NUnit.Framework;
using OpenQA.Selenium;
using TestAutomationFramework.Core.Configuration;
using TestAutomationFramework.Core.Logging;
using TestAutomationFramework.Core.Utilities;
using TestAutomationFramework.Core.WebDriver;
using TestAutomationFramework.Business.Components;

namespace TestAutomationFramework.Tests
{
    /// <summary>
    /// Clase base abstracta para todos los tests
    /// DRY PRINCIPLE: Centraliza setup y teardown común a todos los tests
    /// SOLID - Open/Closed: Abierta para extensión, cerrada para modificación
    /// </summary>
    [TestFixture]
    public abstract class BaseTest
    {
        protected IWebDriver Driver = null!;
        protected ILogger Logger = null!;
        protected IConfiguration Configuration = null!;
        protected ScreenshotHelper ScreenshotHelper = null!;

        //
        protected NavbarComponent navbar = null!;
        protected CookiesComponent cookies = null!;
        //

        /// <summary>
        /// Se ejecuta UNA VEZ antes de todos los tests de la clase
        /// Ideal para configuración que no cambia entre tests
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Inicializa configuración (Singleton)
            Configuration = ConfigurationManager.Instance;

            // Inicializa logger con el nombre de la clase de test
            Logger = new Logger(GetType().Name);

            Logger.Info("=================================================");
            Logger.Info($"INICIANDO SUITE DE TESTS: {GetType().Name}");
            Logger.Info($"Ambiente: {Configuration.GetEnvironment()}");
            Logger.Info($"Navegador: {Configuration.GetBrowser()}");
            Logger.Info("=================================================");
        }

        /// <summary>
        /// Se ejecuta ANTES de cada test individual
        /// TEMPLATE METHOD PATTERN: Define estructura que subclases pueden extender
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var testName = TestContext.CurrentContext.Test.Name;
            Logger.Info($"▶ INICIANDO TEST: {testName}");
            Logger.Info($"Descripción: {TestContext.CurrentContext.Test.FullName}");

            try
            {
                // Inicializa WebDriver usando Singleton + Factory
                DriverManager.Instance.InitializeDriver();
                Driver = DriverManager.Instance.Driver;

                Driver.Manage().Window.Maximize();

                // Inicializa helper de screenshots
                ScreenshotHelper = new ScreenshotHelper(Logger, Configuration);

                // Navega a la URL base configurada
                DriverManager.Instance.NavigateToBaseUrl();

                Logger.Info($"Test '{testName}' inicializado correctamente");

                // Hook para subclases (Template Method Pattern)
                AdditionalSetup();

                navbar = new NavbarComponent(Driver, Logger);

                cookies = new CookiesComponent(Driver, Logger);
                cookies.AcceptCookiesIfPresent();
            }
            catch (Exception ex)
            {
                Logger.Error($"Error durante el Setup del test '{testName}'", ex);
                throw;
            }
        }

        /// <summary>
        /// Se ejecuta DESPUÉS de cada test individual
        /// Maneja screenshots en caso de fallo
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            var testName = TestContext.CurrentContext.Test.Name;
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;

            Logger.Info($"Estado del test '{testName}': {testStatus}");

            try
            {
                // Si el test falló, captura screenshot con fecha y hora
                if (testStatus == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    Logger.Error($"❌ TEST FALLIDO: {testName}");
                    Logger.Error($"Mensaje: {TestContext.CurrentContext.Result.Message}");

                    // Captura screenshot con timestamp automático
                    var screenshotPath = ScreenshotHelper.TakeScreenshot(Driver, testName);

                    if (!string.IsNullOrEmpty(screenshotPath))
                    {
                        Logger.Info($"Screenshot guardado en: {screenshotPath}");
                        // Adjunta el screenshot al reporte de NUnit
                        TestContext.AddTestAttachment(screenshotPath, "Screenshot del Fallo");
                    }
                }
                else if (testStatus == NUnit.Framework.Interfaces.TestStatus.Passed)
                {
                    Logger.Info($"✓ TEST EXITOSO: {testName}");
                }

                // Hook para subclases
                AdditionalTearDown();
            }
            catch (Exception ex)
            {
                Logger.Error($"Error durante TearDown del test '{testName}'", ex);
            }
            finally
            {
                // Siempre cierra el navegador
                DriverManager.Instance.QuitDriver();
                Logger.Info($"▣ TEST FINALIZADO: {testName}");
                Logger.Info("─────────────────────────────────────────────────");
            }
        }

        /// <summary>
        /// Se ejecuta UNA VEZ después de todos los tests de la clase
        /// </summary>
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Logger.Info("=================================================");
            Logger.Info($"FINALIZANDO SUITE DE TESTS: {GetType().Name}");
            Logger.Info("=================================================");
        }

        #region Template Methods - Para que subclases personalicen

        /// <summary>
        /// TEMPLATE METHOD: Permite a clases hijas agregar setup adicional
        /// YAGNI Principle: Solo se implementa en subclases que lo necesiten
        /// </summary>
        protected virtual void AdditionalSetup()
        {
            // Implementación vacía por defecto
            // Las subclases pueden override si necesitan setup adicional
        }

        /// <summary>
        /// TEMPLATE METHOD: Permite a clases hijas agregar teardown adicional
        /// </summary>
        protected virtual void AdditionalTearDown()
        {
            // Implementación vacía por defecto
            // Las subclases pueden override si necesitan teardown adicional
        }

        #endregion

        #region Helper Methods para Tests

        /// <summary>
        /// Helper para capturar screenshot manual en cualquier punto del test
        /// </summary>
        protected void TakeScreenshot(string reason)
        {
            var testName = TestContext.CurrentContext.Test.Name;
            ScreenshotHelper.TakeScreenshot(Driver, testName, reason);
        }

        #endregion
    }
}