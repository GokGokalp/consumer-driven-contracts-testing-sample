using System.Collections.Generic;
using Contracts;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Xunit;

namespace CustomerApiServiceConsumer.Tests
{
    public class CustomerApiConsumerTests : IClassFixture<ConsumerCustomerApiPact>
    {
        private readonly IMockProviderService _mockProviderService;
        private readonly string _mockProviderServiceBaseUri;

        public CustomerApiConsumerTests(ConsumerCustomerApiPact data)
        {
            _mockProviderService = data.MockProviderService;
            _mockProviderServiceBaseUri = data.MockProviderServiceBaseUri;

            data.MockProviderService.ClearInteractions();
        }

        [Fact]
        public void GetCustomer_WhenTheCustomerIdGreaterThanZero_ReturnCustomer()
        {
            //Arrange
            int customerId = 1;

            _mockProviderService
                .Given("There is a customer with id 1")
                .UponReceiving("A GET request to retrieve the customer")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = "/api/customers/" + customerId,
                    Headers = new Dictionary<string, string>
                    {
                        {"Accept", "application/json"}
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, string>
                    {
                        {"Content-Type", "application/json; charset=utf-8"}
                    },
                    Body = new Customer
                    {
                        Id = 1,
                        FullName = "Gökhan Gökalp"
                    }
                });

            var consumer = new CustomerApiClient(_mockProviderServiceBaseUri);

            //Act
            var result = consumer.Get(1);

            //Assert
            Assert.NotNull(result);

            _mockProviderService.VerifyInteractions();
        }
    }
}