namespace RealEstateAgency.Infrastructure.ExternalServiceProxies.FundaPartnerApi.Models
{
    /// <summary>
    /// This class purpose is only for converting the received json to the .NET object that's why it is internal
    /// In this way we can parse only those properties that we're interested in
    /// I've choosen a MakelaarNaam and paging information
    /// </summary>
    internal class JsonResponse
    {
        public ObjectResponse[] Objects { get; set; }
        public PagingResponse Paging { get; set; }
        
        internal class PagingResponse
        {
            public int AantalPaginas { get; set; }
            public int HuidigePagina { get; set; }
        }

        internal class ObjectResponse
        {
            public string MakelaarNaam { get; set; }
        }
    }
}