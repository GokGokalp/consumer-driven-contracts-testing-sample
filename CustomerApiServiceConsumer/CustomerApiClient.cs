using Entities;
using RestSharp;

namespace CustomerApiServiceConsumer
{
    public class CustomerApiClient
    {
        private readonly RestClient _restClient;

        public CustomerApiClient(string baseUri)
        {
            _restClient = new RestClient(baseUri);
        }

        public Customer Get(int id)
        {
            var request = new RestRequest($"/api/customers/{id}", Method.GET) {RequestFormat = DataFormat.Json};
            request.AddHeader("Accept", "application/json");

            var result = _restClient.Execute<Customer>(request);

            return result.Data;
        }
    }
}