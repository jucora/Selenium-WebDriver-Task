using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Reqnroll.Formatters.PayloadProcessing.Cucumber;
using RestSharp;
using System.Net;
using TestAutomationFramework.Business.Api.DTO;
using TestAutomationFramework.Core.Configuration.Api;
using TestAutomationFramework.Core.Logging;

namespace TestAutomationFramework.Tests.ApiTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [Category("API")]
    public class ApiTests
    {
        private RestClient _client = null!;
        private ILogger Logger = null!;
        private UsersApiClient _usersApiClient = null!;
        private readonly IApiConfiguration _apiConfiguration = ApiConfigurationManager.Instance;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Logger = new Logger(GetType().Name);

            Logger.Info("=================================================");
            Logger.Info($"STARTING TEST: {GetType().Name}");
            Logger.Info("=================================================");
        }

        [SetUp]
        public void Setup()
        {
            _client = new RestClient(_apiConfiguration.GetBaseUrl());
            _usersApiClient = new UsersApiClient(_client);
            Logger.Info("Test started");
        }

        [Test]
        public void Validate_List_Of_Users_Can_Be_Received()
        {
            Logger.Info("Sending GET /users");

            var response = _usersApiClient.GetUsers();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var users = _usersApiClient.GetUsersList(response);

            Assert.That(users, Is.Not.Null, "Users list is null");
            Assert.That(users, Is.Not.Empty, "Users list is empty");

            foreach (var user in users!)
            {
                Logger.Info($"Validating user with ID {user.Id}");

                Assert.Multiple(() =>
                {
                    Assert.That(user.Id, Is.GreaterThan(0), "User Id is invalid");
                    Assert.That(user.Name, Is.Not.Empty, $"Name is empty for user {user.Id}");
                    Assert.That(user.Username, Is.Not.Empty, $"Username is empty for user {user.Id}");
                    Assert.That(user.Email, Is.Not.Empty, $"Email is empty for user {user.Id}");
                    Assert.That(user.Phone, Is.Not.Empty, $"Phone is empty for user {user.Id}");
                    Assert.That(user.Website, Is.Not.Empty, $"Website is empty for user {user.Id}");

                    Assert.That(user.Address, Is.Not.Null, $"Address is null for user {user.Id}");
                    Assert.That(user.Company, Is.Not.Null, $"Company is null for user {user.Id}");
                    Assert.That(user.Company.Name, Is.Not.Empty, $"Company name is empty for user {user.Id}");
                });
            }

            Logger.Info("Users list validated successfully");
        }

        [Test]
        public void Validate_Response_Header_ContentType()
        {
            Logger.Info("Validating response headers");

            var response = _usersApiClient.GetUsers();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var contentType = response.ContentHeaders != null
                ? response.ContentHeaders.FirstOrDefault(h => h.Name == "Content-Type")?.Value?.ToString()
                : null;

            Assert.That(contentType, Is.EqualTo("application/json; charset=utf-8"));

            Logger.Info("Content-Type header validated");
        }

        [Test]
        public void Validate_Users_Content()
        {
            Logger.Info("Validating users content");

            var response = _usersApiClient.GetUsers();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var users = JArray.Parse(
                response.Content ?? throw new InvalidOperationException("Response content is null"));

            Assert.That(users.Count, Is.EqualTo(10));
            Assert.That(users.Select(u => u["id"]?.Value<int>() ?? -1).Distinct().Count(), Is.EqualTo(10));

            foreach (var user in users)
            {
                Assert.That(user["name"]?.ToString(), Is.Not.Empty);
                Assert.That(user["username"]?.ToString(), Is.Not.Empty);
                Assert.That(user["company"]?["name"]?.ToString(), Is.Not.Empty);
            }

            Logger.Info("Users content validated");
        }

        [Test]
        public void Validate_User_Can_Be_Created()
        {
            Logger.Info("Creating new user");

            var response = _usersApiClient.CreateUser();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var createdUser = _usersApiClient.GetCreatedUser(response);

            Assert.That(createdUser.Id, Is.Not.Null);

            Logger.Info($"User created with ID: {createdUser.Id}");
        }

        [Test]
        public void Validate_Resource_Not_Found()
        {
            Logger.Info("Sending request to invalid endpoint");

            var request = new RestRequestBuilder("/invalidendpoint", Method.Get).Build();
            var response = _client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

            Logger.Info("404 Not Found validated");
        }
    }
}
