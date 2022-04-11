namespace RealEstateAgency.Services.Models
{
    public class GetTopRealEstateAgenciesDto
    {
        public int Take { get; set; }
        public ApartmentFeatures? ApartmentsFeature { get; set; }
    }
}