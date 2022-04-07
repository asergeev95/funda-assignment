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

        [HttpGet]
        public async Task<object> GetTopTenRealEstateAgencies()
        {

            var result = await _agencyService.GetTopTenRealEstateAgencies();
            return result;
        }
        //
        // [HttpPost]
        // public object GetTopTenRealEstateAgenciesWithGardens()
        // {
        //     var result = _agencyService.GetTopTenRealEstateAgenciesWithGardens();
        //     return null;
        // }
        
    }
}