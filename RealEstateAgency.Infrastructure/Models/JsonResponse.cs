namespace RealEstateAgency.Infrastructure.Models
{
    internal class JsonResponse
    {
        public int AccountStatus { get; set; }
        public bool EmailNotConfirmed { get; set; }
        public bool ValidationFailed { get; set; }
        public int Website { get; set; }
        public MetadataResponse Metadata { get; set; }
        public ObjectResponse[] Objects { get; set; }

        internal class ObjectResponse
        {
            public string AangebodenSindsTekst { get; set; }
            public string AanmeldDatum { get; set; }
            public double? AantalBeschikbaar { get; set; }
            public double AantalKamers { get; set; }
            public double? AantalKavels { get; set; }
            public string Aanvaarding { get; set; }
            public string Adres { get; set; }
            public int Afstand { get; set; }
            public string BronCode { get; set; }
            public string MakelaarNaam { get; set; }
        }
        
        internal class MetadataResponse
        {
            public string ObjectType { get; set; }
            public string Omschrijving { get; set; }
            public string Titel { get; set; }
        }
    }
}