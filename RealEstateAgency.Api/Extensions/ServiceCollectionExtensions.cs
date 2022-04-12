using System;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RealEstateAgency.Api.Validators;
using RealEstateAgency.Infrastructure.ExternalServiceProxies.FundaPartnerApi;
using RealEstateAgency.Services.Implementations;
using RealEstateAgency.Services.Interfaces;

namespace RealEstateAgency.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection RegisterCommon(this IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                ).AddFluentValidation(
                    config =>
                    {
                        config.RegisterValidatorsFromAssemblyContaining<V1GetTopRentalAgenciesRequestValidator>();
                    });
            services.AddSwaggerGen(opts =>
                opts.SwaggerDoc("v1", new OpenApiInfo {Title = "Real estate information", Version = "v1"}));
            return services;
        }
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRealEstateAgencyService, RealEstateAgencyService>();
            services.AddHttpClient<IFundaPartnerApiClient, FundaPartnerApiClient>(c =>
            {
                var url = configuration.GetValue<string>("ExternalServices:FundaPartnerApi:Url");
                var key = configuration.GetValue<string>("ExternalServices:FundaPartnerApi:Key");
                c.BaseAddress = new Uri($"{url}/{key}");
            });
            return services;
        }
    }
}