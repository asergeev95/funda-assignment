using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstateAgency.Infrastructure.ExternalServiceProxies.FundaPartnerApi;

namespace RealEstateAgency.Infrastructure.Tests.Bootstrap
{
    public static class Registrations
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHttpClient<IFundaPartnerApiClient, FundaPartnerApiClient>(c =>
            {
                var url = configuration["ExternalServices:FundaPartnerApi:Url"];
                var key = configuration["ExternalServices:FundaPartnerApi:Key"];
                c.BaseAddress = new Uri($"{url}/{key}");
            });
            return services;
        }
    }
}
