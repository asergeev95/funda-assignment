using System.Threading.Tasks;
using RealEstateAgency.Infrastructure.ExternalServiceProxies.FundaPartnerApi.Contracts;

namespace RealEstateAgency.Infrastructure.ExternalServiceProxies.FundaPartnerApi
{
    public interface IFundaPartnerApiClient
    {
        Task<GetRealEstatesResult> GetRealEstates(int pageSize = 500, bool withTuin = false);
    }
}