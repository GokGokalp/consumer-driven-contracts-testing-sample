using CustomerApi.Host;
using Microsoft.Owin.Testing;
using PactNet;
using Xunit;

namespace CustomerApi.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void EnsureCustomerApiHonoursPactWithConsumer()
        {
            //Arrange
            IPactVerifier pactVerifier = new PactVerifier(() => { }, () => { });

            pactVerifier
                .ProviderState("There is a customer with id 1");

            //Act / Assert
            using (var testServer = TestServer.Create<Startup>())
            {
                pactVerifier
                .ServiceProvider("CustomerApi.Host", testServer.HttpClient)
                .HonoursPactWith("CustomerApiServiceConsumer")
                .PactUri("../../../CustomerApiServiceConsumer.Tests/pacts/customerapiserviceconsumer-customerapi.host.json")
                .Verify();
            }
        }
    }
}