using System.Threading.Tasks;

namespace RealEstateAgency.Services.Interfaces
{
    public interface IRealEstateAgencyService
    {
        Task<object> GetTopTenRealEstateAgencies();
        Task<object> GetTopTenRealEstateAgenciesWithGardens();
    }
}