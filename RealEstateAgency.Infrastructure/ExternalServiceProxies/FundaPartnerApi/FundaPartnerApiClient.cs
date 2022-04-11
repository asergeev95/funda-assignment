using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
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

        public async Task<Result<GetRealEstatesResult>> GetRealEstates(GetRealEstatesDto dto)
        {
            var currentPage = 1;
            BuildRequestUrls(dto, currentPage, out var selectedCity, out var selectedFeatures,  out var url);

            var realEstatesObjects = new List<JsonResponse.ObjectResponse>();
            try
            {
                var response = await GetResponseAsync<JsonResponse>(url, HttpMethod.Get);
                if (response.IsSuccess)
                {
                    realEstatesObjects.AddRange(response.Value.Objects);
                    while (currentPage < response.Value.Paging.AantalPaginas)
                    {
                        currentPage += 1;
                        var paging = $"/&page={currentPage}&pagesize={dto.PageSize}";
                        url = $"/?type=koop&zo={selectedCity}{selectedFeatures}{paging}";
                        response = await GetResponseAsync<JsonResponse>(url, HttpMethod.Get);
                        realEstatesObjects.AddRange(response.Value.Objects);
                    }
                }

                return Result.Ok(new GetRealEstatesResult
                {
                    RealEstateAdvts = realEstatesObjects.Select(o => new GetRealEstatesResult.RealEstateAdvt
                    {
                        Makelaar = o.MakelaarNaam
                    }).ToArray()
                });
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }

        private static void BuildRequestUrls(GetRealEstatesDto dto, int currentPage, out string selectedCity, out string searchFeature, out string url)
        {
            var paging = $"/&page={currentPage}&pagesize={dto.PageSize}";
            var selectedFeatures = BuildSelectedFeatures(dto);
            selectedCity = "/amsterdam";
            url = $"/?type=koop&zo={selectedCity}{selectedFeatures}{paging}";
            searchFeature = selectedFeatures;
        }

        private static string BuildSelectedFeatures(GetRealEstatesDto dto)
        {
            var selectedFeatures = string.Empty;
            if (dto.WithBalcon)
            {
                selectedFeatures = $"/balcon";
            }

            if (dto.WithDakterras)
            {
                selectedFeatures = $"{selectedFeatures}/dakterras";
            }

            if (dto.WithTuin)
            {
                selectedFeatures = $"{selectedFeatures}/tuin";
            }

            return selectedFeatures;
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