namespace RealEstateAgency.Api.Responses
{
    public class V1GetTopRentalAgenciesResponse
    {
        public RealEstateInfo[] RealEstateAgencyInfo { get; set; }

        public class RealEstateInfo
        {
            public string Name { get; set; }
            public int AdvtsCount { get; set; }
        }
    }
}