using System.Threading.Tasks;
using FluentResults;
using RealEstateAgency.Services.Models;

namespace RealEstateAgency.Services.Interfaces
{
    public interface IRealEstateAgencyService
    {
        Task<Result<GetTopRealEstateAgenciesResult>> GetTopRealEstateAgencies(GetTopRealEstateAgenciesDto dto);
    }
}