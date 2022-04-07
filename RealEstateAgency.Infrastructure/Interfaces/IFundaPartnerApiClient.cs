using System.Threading.Tasks;
using RealEstateAgency.Infrastructure.Contracts;

namespace RealEstateAgency.Infrastructure.Interfaces
{
    public interface IFundaPartnerApiClient
    {
        Task<GetRealEstatesResult> GetRealEstates(int pageSize = 500, bool withTuin = false);
    }
}