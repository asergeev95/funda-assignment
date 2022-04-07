using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RealEstateAgency.Infrastructure.Implementations;
using RealEstateAgency.Infrastructure.Interfaces;
using RealEstateAgency.Services.Implementations;
using RealEstateAgency.Services.Interfaces;

namespace ReatEstateAgency.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(opts => opts.SwaggerDoc("v1", new OpenApiInfo{ Title = "Real estate information", Version = "v1" }));

            services.AddScoped<IRealEstateAgencyService, RealEstateAgencyService>();
            services.AddHttpClient<IFundaPartnerApiClient, FundaPartnerApiClient>(c =>
            {
                var url = Configuration.GetValue<string>("ExternalServices:FundaPartnerApi:Url");
                var key = Configuration.GetValue<string>("ExternalServices:FundaPartnerApi:Key");
                c.BaseAddress = new Uri($"{url}/{key}");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(opts => opts.SwaggerEndpoint("/swagger/v1/swagger.json", "Real estate information swagger"));
            
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
