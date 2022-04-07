namespace RealEstateAgency.Infrastructure.ExternalServiceProxies.FundaPartnerApi.Contracts
{
    public class GetRealEstatesResult
    {
        public RealEstateAdvt[] RealEstateAdvts { get; set; }

        public class RealEstateAdvt
        {
            public string Makelaar { get; set; }
        }
    }
}