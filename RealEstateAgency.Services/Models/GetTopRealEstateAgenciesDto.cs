namespace RealEstateAgency.Services.Models
{
    public class GetTopRealEstateAgenciesDto
    {
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public ApartmentFeatures? ApartmentsFeature { get; set; }
    }
}