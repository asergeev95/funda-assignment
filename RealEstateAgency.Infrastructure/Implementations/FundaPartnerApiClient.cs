using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RealEstateAgency.Infrastructure.Contracts;
using RealEstateAgency.Infrastructure.Interfaces;
using RealEstateAgency.Infrastructure.Models;
using Serilog;

namespace RealEstateAgency.Infrastructure.Implementations
{
    public class FundaPartnerApiClient : IFundaPartnerApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerSettings _serializerSettings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public FundaPartnerApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetRealEstatesResult> GetRealEstates()
        {
            var url = "/?type=koop&zo=/amsterdam/tuin/&page=1&pagesize=25";
            var response = await GetResponseAsync<JsonResponse>(url, HttpMethod.Get);
            
            return new GetRealEstatesResult()
            {
                RealEstateAdvts = response.Value.Objects.Select(o => new GetRealEstatesResult.RealEstateAdvt()
                {
                    Makelaar = o.MakelaarNaam
                }).ToArray()
            };
        }
        
        private async Task<(bool IsSuccess, string FaultMessage, T Value)> GetResponseAsync<T>(string url, HttpMethod method, string content = null)
            where T : class
        {
            try
            {
                var httpRequest = new HttpRequestMessage
                {
                    Method = method,
                    RequestUri = new Uri($"{_httpClient.BaseAddress}{url}"),
                    Content = new StringContent(content ?? string.Empty, Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(httpRequest);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return (true, null, JsonConvert.DeserializeObject<T>(json, _serializerSettings));
                }

                var body = await response.Content.ReadAsStringAsync();
                return (false, body, null);
            }
            catch (Exception e)
            {
                Log.Error("An error occured during airport service request, {@exception}", e);
                throw;
            }
        }
    }
}