using RestSharp;

namespace TestAutomationFramework.Tests.ApiTests
{
    public class RestRequestBuilder
    {
        private readonly RestRequest _request;

        public RestRequestBuilder(string resource, Method method)
        {
            _request = new RestRequest(resource, method);
        }

        public RestRequestBuilder AddHeader(string key, string value)
        {
            _request.AddHeader(key, value);
            return this;
        }

        public RestRequestBuilder AddJsonBody(object body)
        {
            _request.AddJsonBody(body);
            return this;
        }

        public RestRequest Build()
        {
            return _request;
        }
    }
}
