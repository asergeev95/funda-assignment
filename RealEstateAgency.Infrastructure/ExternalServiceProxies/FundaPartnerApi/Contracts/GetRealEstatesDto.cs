namespace RealEstateAgency.Infrastructure.ExternalServiceProxies.FundaPartnerApi.Contracts
{
    public class GetRealEstatesDto
    {
        public int PageSize { get; set; }
        public bool WithTuin { get; set; }
        public bool WithBalcon { get; set; }
        public bool WithDakterras { get; set; }
    }
}