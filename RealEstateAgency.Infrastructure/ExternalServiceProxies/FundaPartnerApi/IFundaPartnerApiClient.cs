using System.Threading.Tasks;
using FluentResults;
using RealEstateAgency.Infrastructure.ExternalServiceProxies.FundaPartnerApi.Contracts;

namespace RealEstateAgency.Infrastructure.ExternalServiceProxies.FundaPartnerApi
{
    public interface IFundaPartnerApiClient
    {
        Task<Result<GetRealEstatesResult>> GetRealEstates(GetRealEstatesDto dto);
    }
}