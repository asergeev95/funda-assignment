using Microsoft.Extensions.DependencyInjection;
using RealEstateAgency.Infrastructure.ExternalServiceProxies.FundaPartnerApi;
using RealEstateAgency.Services.Implementations;
using RealEstateAgency.Services.Interfaces;
using RealEstateAgency.Services.Tests.Mocks;

namespace RealEstateAgency.Services.Tests.Bootstrap
{
    public static class Registrations
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {

            services.AddHttpClient<IFundaPartnerApiClient, FundaPartnerApiClientMock>();
            services.AddScoped<IRealEstateAgencyService, RealEstateAgencyService>();
            return services;
        }
    }
}
