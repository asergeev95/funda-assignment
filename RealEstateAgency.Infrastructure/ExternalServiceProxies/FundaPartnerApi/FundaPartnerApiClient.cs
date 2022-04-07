using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RealEstateAgency.Infrastructure.ExternalServiceProxies.FundaPartnerApi.Contracts;
using RealEstateAgency.Infrastructure.ExternalServiceProxies.FundaPartnerApi.Models;
using Serilog;

namespace RealEstateAgency.Infrastructure.ExternalServiceProxies.FundaPartnerApi
{
    public class FundaPartnerApiClient : IFundaPartnerApiClient
    {
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerSettings _serializerSettings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize
        };

        public FundaPartnerApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetRealEstatesResult> GetRealEstates(int pageSize = 500, bool withTuin = false)
        {
            string url;
            var currentPage = 1;
            url = withTuin ? $"/?type=koop&zo=/amsterdam/tuin/&page={currentPage}&pagesize={pageSize}" 
                : $"/?type=koop&zo=/amsterdam/&page={currentPage}&pagesize={pageSize}";

            var realEstatesObjects = new List<JsonResponse.ObjectResponse>();
            var response = await GetResponseAsync<JsonResponse>(url, HttpMethod.Get);
            if (response.IsSuccess)
            {
                realEstatesObjects.AddRange(response.Value.Objects);
                while (currentPage < response.Value.Paging.AantalPaginas)
                {
                    currentPage += 1;
                    url = withTuin ? $"/?type=koop&zo=/amsterdam/tuin/&page={currentPage}&pagesize={pageSize}" 
                        : $"/?type=koop&zo=/amsterdam/&page={currentPage}&pagesize={pageSize}";
                    response = await GetResponseAsync<JsonResponse>(url, HttpMethod.Get);
                    realEstatesObjects.AddRange(response.Value.Objects);
                }
            }
            
            return new GetRealEstatesResult
            {
                RealEstateAdvts = realEstatesObjects.Select(o => new GetRealEstatesResult.RealEstateAdvt
                {
                    Makelaar = o.MakelaarNaam
                }).ToArray()
            };
        }

        private async Task<(bool IsSuccess, string FaultMessage, T Value)> GetResponseAsync<T>(string url,
            HttpMethod method, string content = null)
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