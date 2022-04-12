using System.Diagnostics;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using RealEstateAgency.Infrastructure.ExternalServiceProxies.FundaPartnerApi;
using RealEstateAgency.Infrastructure.ExternalServiceProxies.FundaPartnerApi.Contracts;
using RealEstateAgency.Infrastructure.Tests.Bootstrap;
using Xunit;

namespace RealEstateAgency.Infrastructure.Tests
{
    
    public class FundaPartnerApiClientTests
    {
        [Theory]
        [InlineData(false, false, false)]
        [InlineData(true, true, true)]
        public async Task ShouldCorrectlySendRequest(bool withBalcon, bool withDakterras, bool withTuin)
        {
            var service = GetService();
            var dto = new GetRealEstatesDto
            {
                PageSize = 100,
                WithBalcon = withBalcon,
                WithDakterras = withDakterras,
                WithTuin = withTuin
            };
            var request = await service.GetRealEstates(dto);
            request.IsSuccess.Should().BeTrue();
        }
        
        [DebuggerStepThrough]
        private static IFundaPartnerApiClient GetService()
        {
            return CompositionRoot.ServiceProvider.GetRequiredService<IFundaPartnerApiClient>();
        }
    }
}