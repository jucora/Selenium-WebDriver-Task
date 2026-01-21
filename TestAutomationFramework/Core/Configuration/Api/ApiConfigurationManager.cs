namespace TestAutomationFramework.Core.Configuration.Api
{
    public sealed class ApiConfigurationManager : BaseConfigurationManager, IApiConfiguration
    {
        private static readonly Lazy<ApiConfigurationManager> instance =
            new(() => new ApiConfigurationManager());

        public static ApiConfigurationManager Instance => instance.Value;

        private ApiConfigurationManager()
            : base(AppContext.BaseDirectory + "Tests/Configuration/Api",
                   "apisettings.json")
        { }

        public string GetBaseUrl() => GetValue("BaseUrl");
    }
}
