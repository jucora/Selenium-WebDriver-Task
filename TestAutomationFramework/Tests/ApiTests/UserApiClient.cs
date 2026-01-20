using Newtonsoft.Json;
using RestSharp;
using TestAutomationFramework.Business.Api.DTO;
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

        public RestResponse CreateUser()
        {
            _logger.Info("POST /users");

            var user = new UserDto
            {
                Name = "John Doe",
                Username = "jdoe"
            };

            var request = new RestRequestBuilder("/users", Method.Post)
                .AddJsonBody(user)
                .Build();

            return _client.Execute(request);
        }

        public List<UserDto> GetUsersList(RestResponse restResponse)
        {
            _logger.Info("Deserializing users list from response");

            if (string.IsNullOrWhiteSpace(restResponse.Content))
                throw new InvalidOperationException("Response content is null or empty");

            return JsonConvert.DeserializeObject<List<UserDto>>(restResponse.Content)!;
        }

        public UserDto GetCreatedUser(RestResponse restResponse)
        {
            _logger.Info("Deserializing users list from response");

            if (string.IsNullOrWhiteSpace(restResponse.Content))
                throw new InvalidOperationException("Response content is null or empty");

            return JsonConvert.DeserializeObject<UserDto>(restResponse.Content)!;
        }
    }
}
