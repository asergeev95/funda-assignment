using JetBrains.Annotations;

namespace RealEstateAgency.Api.Requests
{
    public class V1GetTopRentalAgenciesRequest
    {
        /// <summary>
        /// See enum <see cref="RealEstateAgency.Services.Models.ApartmentFeatures"/> for possible values.
        /// </summary>
        
        [CanBeNull]
        public string ApartmentFeatures { get; set; }
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}