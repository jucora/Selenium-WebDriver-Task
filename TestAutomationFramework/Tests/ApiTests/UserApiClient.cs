using RestSharp;
using TestAutomationFramework.Core.Logging;

namespace TestAutomationFramework.Tests.ApiTests
{
    public class UsersApiClient
    {
        private readonly RestClient _client;
        private readonly ILogger _logger = new Logger(typeof(UsersApiClient).Name);

        public UsersApiClient(RestClient client)
        {
            _client = client;
        }

        public RestResponse GetUsers()
        {
            _logger.Info("GET /users");

            var request = new RestRequestBuilder("/users", Method.Get).Build();
            return _client.Execute(request);
        }

        public RestResponse CreateUser(object user)
        {
            _logger.Info("POST /users");

            var request = new RestRequestBuilder("/users", Method.Post)
                .AddJsonBody(user)
                .Build();

            return _client.Execute(request);
        }
    }
}
