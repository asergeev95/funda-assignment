using System;

namespace RealEstateAgency.Services.Models
{
    [Flags]
    public enum ApartmentFeatures
    {
        Balcon = 1,
        Dakterras = 2,
        Tuin = 4
    }
}