using System.Linq;
using System.Threading.Tasks;
using RealEstateAgency.Infrastructure.Interfaces;
using RealEstateAgency.Services.Interfaces;

namespace RealEstateAgency.Services.Implementations
{
    public class RealEstateAgencyService : IRealEstateAgencyService
    {
        private readonly IFundaPartnerApiClient _apiClient;

        public RealEstateAgencyService(IFundaPartnerApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<object> GetTopTenRealEstateAgencies()
        {
            var result = await _apiClient.GetRealEstates(pageSize: 500, withTuin: false);
            var orderedAgents = result.RealEstateAdvts.GroupBy(x => x.Makelaar).Select(group => new
            {
                Makelaar = group.Key,
                NumberOfAdvts = group.Count()
            }).OrderByDescending(x => x.NumberOfAdvts).Take(10).ToArray();
            return orderedAgents;
        }

        public async Task<object> GetTopTenRealEstateAgenciesWithGardens()
        {
            var result = await _apiClient.GetRealEstates(pageSize: 500, withTuin: true);
            var orderedAgents = result?.RealEstateAdvts.GroupBy(x => x.Makelaar).Select(group => new
            {
                Makelaar = group.Key,
                NumberOfAdvts = group.Count()
            }).OrderByDescending(x => x.NumberOfAdvts).Take(10).ToArray();

            return orderedAgents;
        }
    }
}