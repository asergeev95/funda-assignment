using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using RealEstateAgency.Api.Requests;
using RealEstateAgency.Api.Responses;
using RealEstateAgency.IntegrationTests.Bootstrap;
using Xunit;

namespace RealEstateAgency.IntegrationTests
{
    [Collection("Integration")]
    public class GetTopRealEstateAgenciesTests
    {
        private readonly HttpClient _client;
        public GetTopRealEstateAgenciesTests(IntegrationFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task GivenCorrectV1GetTopRentalAgenciesRequest_WhenGetTopRealEstateAgencies_ReturnProperResponse()
        {
            var request = new V1GetTopRentalAgenciesRequest();
            using var receivedResponse = await _client.PostAsJsonAsync($"/api/v1/real-estate/top-agents", request);
            receivedResponse.IsSuccessStatusCode.Should().BeTrue();
            var response = await receivedResponse.Content.ReadAsAsync<V1GetTopRentalAgenciesResponse>();
            response.Should().NotBeNull();
            response.RealEstateAgencyInfo.Should().NotBeNull();
            var agencyInfo = response.RealEstateAgencyInfo;
            agencyInfo.Should().NotBeEmpty();
        }
        
        [Fact]
        public async Task GivenV1GetTopRentalAgenciesRequestWithNegativeTake_ReturnBadRequest()
        {
            var request = new V1GetTopRentalAgenciesRequest()
            {
                Take = -1
            };
            using var receivedResponse = await _client.PostAsJsonAsync($"/api/v1/real-estate/top-agents", request);
            receivedResponse.IsSuccessStatusCode.Should().BeFalse();
            receivedResponse.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
        
        [Theory]
        [InlineData("Tuin1")]
        [InlineData("Balcon1")]
        [InlineData("Dakterras1")]
        public async Task GivenV1GetTopRentalAgenciesRequestWithIncorrectFeatureValue_ReturnBadRequest(string apartmentsFeatures)
        {
            var request = new V1GetTopRentalAgenciesRequest
            {
                ApartmentFeatures = apartmentsFeatures
            };
            using var receivedResponse = await _client.PostAsJsonAsync($"/api/v1/real-estate/top-agents", request);
            receivedResponse.IsSuccessStatusCode.Should().BeFalse();
            receivedResponse.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}