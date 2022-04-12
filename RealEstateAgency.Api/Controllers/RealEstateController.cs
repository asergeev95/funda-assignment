using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Api.Requests;
using RealEstateAgency.Api.Responses;
using RealEstateAgency.Services.Interfaces;
using RealEstateAgency.Services.Models;

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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("top-agents")]
        public async Task<ActionResult<V1GetTopRentalAgenciesResponse>> GetTopTenRealEstateAgencies(V1GetTopRentalAgenciesRequest request)
        {

            var result = await _agencyService.GetTopRealEstateAgencies(
                new GetTopRealEstateAgenciesDto
                {
                    Take = request.Take ?? 10,
                    ApartmentsFeature = string.IsNullOrEmpty(request.ApartmentFeatures) ? 
                        default(ApartmentFeatures?) : 
                        Enum.Parse<ApartmentFeatures>(request.ApartmentFeatures, true)
                });
            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }
            return Ok(new V1GetTopRentalAgenciesResponse
            {
                RealEstateAgencyInfo = result.Value.RealEstatesInfo.Select(x => new V1GetTopRentalAgenciesResponse.RealEstateInfo
                {
                    Name = x.Name,
                    AdvtsCount = x.AdvtsCount
                }).ToArray()
            });
        }
    }
}