using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Net;
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

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Initializes logger with the test class name
            Logger = new Logger(GetType().Name);

            Logger.Info("=================================================");
            Logger.Info($"STARTING TEST: {GetType().Name}");
            Logger.Info("=================================================");
        }

        [SetUp]
        public void Setup()
        {
            _client = new RestClient("https://jsonplaceholder.typicode.com");
            Logger.Info("Test started");
        }

        [Test]
        public void Validate_List_Of_Users_Can_Be_Received()
        {
            Logger.Info("Sending GET /users");

            var request = new RestRequestBuilder("/users", Method.Get)
                .Build();

            var response = _client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var users = JArray.Parse(
                response.Content ?? throw new InvalidOperationException("Response content is null"));

            var requiredFields = new[]
            {
                "id","name","username","email","address","phone","website","company"
            };

            foreach (var user in users)
            {
                foreach (var field in requiredFields)
                {
                    Logger.Info($"Validating field '{field}' for user ID {user["id"]}");
                    Assert.That(user[field], Is.Not.Null);
                }
            }

            Logger.Info("Users list validated successfully");
        }

        [Test]
        public void Validate_Response_Header_ContentType()
        {
            Logger.Info("Validating response headers");

            var request = new RestRequestBuilder("/users", Method.Get).Build();
            var response = _client.Execute(request);

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

            var request = new RestRequestBuilder("/users", Method.Get).Build();
            var response = _client.Execute(request);

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

            var request = new RestRequestBuilder("/users", Method.Post)
                .AddJsonBody(new
                {
                    name = "John Doe",
                    username = "jdoe"
                })
                .Build();

            var response = _client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var json = JObject.Parse(
                response.Content ?? throw new InvalidOperationException("Response content is null"));

            Assert.That(json["id"], Is.Not.Null);

            Logger.Info($"User created with ID: {json["id"]}");
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
