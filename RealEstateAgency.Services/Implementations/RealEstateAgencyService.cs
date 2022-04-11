using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using RealEstateAgency.Infrastructure.ExternalServiceProxies.FundaPartnerApi;
using RealEstateAgency.Infrastructure.ExternalServiceProxies.FundaPartnerApi.Contracts;
using RealEstateAgency.Services.Interfaces;
using RealEstateAgency.Services.Models;

namespace RealEstateAgency.Services.Implementations
{
    public class RealEstateAgencyService : IRealEstateAgencyService
    {
        private readonly IFundaPartnerApiClient _apiClient;

        public RealEstateAgencyService(IFundaPartnerApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<Result<GetTopRealEstateAgenciesResult>> GetTopRealEstateAgencies(GetTopRealEstateAgenciesDto dto)
        {
            var getRealEstatesDto = BuildGetRealEstatesDto(dto);
            var result = await _apiClient.GetRealEstates(getRealEstatesDto);
            if (result.IsSuccess)
            {
                var orderedAgents = result.Value.RealEstateAdvts.GroupBy(x => x.Makelaar).Select(group => new
                {
                    Makelaar = group.Key,
                    NumberOfAdvts = group.Count()
                }).OrderByDescending(x => x.NumberOfAdvts).Take(dto.Take).ToArray();
                return Result.Ok(new GetTopRealEstateAgenciesResult
                {
                    RealEstateAgencyName = orderedAgents.Select(x => new GetTopRealEstateAgenciesResult.RealEstateInfo()
                    {
                        Name = x.Makelaar,
                        AdvtsCount = x.NumberOfAdvts
                    }).ToArray()
                });    
            }

            return Result.Fail(result.Errors.First());

        }

        private static GetRealEstatesDto BuildGetRealEstatesDto(GetTopRealEstateAgenciesDto dto)
        {
            var apartmentFeatures = dto.ApartmentsFeature;
            var featuresSelected = apartmentFeatures.HasValue;
            var getRealEstatesDto = new GetRealEstatesDto
            {
                PageSize = dto.PageSize,
                WithBalcon = featuresSelected && apartmentFeatures.Value.HasFlag(ApartmentFeatures.Balcon),
                WithDakterras = featuresSelected && apartmentFeatures.Value.HasFlag(ApartmentFeatures.Dakterras),
                WithTuin = featuresSelected && apartmentFeatures.Value.HasFlag(ApartmentFeatures.Tuin)
            };
            return getRealEstatesDto;
        }
    }
}