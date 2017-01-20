using System;
using PactNet;
using PactNet.Mocks.MockHttpService;

namespace CustomerApiServiceConsumer.Tests
{
    public class ConsumerCustomerApiPact : IDisposable
    {
        public IPactBuilder PactBuilder { get; private set; }
        public IMockProviderService MockProviderService { get; private set; }

        public int MockServerPort => 1234;
        public string MockProviderServiceBaseUri => $"http://localhost:{MockServerPort}";

        public ConsumerCustomerApiPact()
        {
            PactBuilder = new PactBuilder();
            PactBuilder.ServiceConsumer("CustomerApiServiceConsumer").HasPactWith("CustomerApi.Host");

            MockProviderService = PactBuilder.MockService(MockServerPort);
        }

        public void Dispose()
        {
            PactBuilder.Build();
        }
    }
}