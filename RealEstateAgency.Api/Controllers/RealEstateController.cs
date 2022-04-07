using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Services.Interfaces;

namespace RealEstateAgency.Api.Controllers
{
    
    [ApiController]
    [Route("api/v1/real-estate")]
    public class RealEstateController : ControllerBase
    {
        private readonly IRealEstateAgencyService _agencyService;

        public RealEstateController(IRealEstateAgencyService agencyService)
        {
            _agencyService = agencyService;
        }

        [HttpGet("top-ten-agents")]
        public async Task<object> GetTopTenRealEstateAgencies()
        {

            var result = await _agencyService.GetTopTenRealEstateAgencies();
            return result;
        }
        
        [HttpGet("top-ten-agents-with-gardens")]
        public async Task<object> GetTopTenRealEstateAgenciesWithGardens()
        {
            var result = await _agencyService.GetTopTenRealEstateAgenciesWithGardens();
            return result;
        }
        
    }
}