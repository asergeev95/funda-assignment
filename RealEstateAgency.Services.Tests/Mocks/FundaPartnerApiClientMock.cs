using System.Net.Http;
using System.Threading.Tasks;
using FluentResults;
using RealEstateAgency.Infrastructure.ExternalServiceProxies.FundaPartnerApi;
using RealEstateAgency.Infrastructure.ExternalServiceProxies.FundaPartnerApi.Contracts;

namespace RealEstateAgency.Services.Tests.Mocks
{
    public class FundaPartnerApiClientMock : IFundaPartnerApiClient
    {
        public static GetRealEstatesResult ValueToReturn = new()
        {
            RealEstateAdvts = new[]
            {
                new GetRealEstatesResult.RealEstateAdvt
                {
                    Makelaar = "MakelaarName1"
                },
                new GetRealEstatesResult.RealEstateAdvt
                {
                    Makelaar = "MakelaarName1"
                },
                new GetRealEstatesResult.RealEstateAdvt
                {
                    Makelaar = "MakelaarName1"
                },
                new GetRealEstatesResult.RealEstateAdvt
                {
                    Makelaar = "MakelaarName2"
                },
                new GetRealEstatesResult.RealEstateAdvt
                {
                    Makelaar = "MakelaarName2"
                },
                new GetRealEstatesResult.RealEstateAdvt
                {
                    Makelaar = "MakelaarName3"
                }
            }
        };

        public FundaPartnerApiClientMock(HttpClient _)
        {
            
        }
        public Task<Result<GetRealEstatesResult>> GetRealEstates(GetRealEstatesDto dto)
        {
            var result = Result.Ok(ValueToReturn);
            return Task.FromResult(result);
        }
    }
}