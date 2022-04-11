namespace RealEstateAgency.Services.Models
{
    public class GetTopRealEstateAgenciesResult
    {
        public RealEstateInfo[] RealEstateAgencyName { get; set; }

        public class RealEstateInfo
        {
            public string Name { get; set; }
            public int AdvtsCount { get; set; }
        }
    }
}