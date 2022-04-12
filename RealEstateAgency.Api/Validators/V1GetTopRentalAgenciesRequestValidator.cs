using System;
using FluentValidation;
using JetBrains.Annotations;
using RealEstateAgency.Api.Requests;
using RealEstateAgency.Services.Models;

namespace RealEstateAgency.Api.Validators
{
    [UsedImplicitly]
    public class V1GetTopRentalAgenciesRequestValidator : AbstractValidator<V1GetTopRentalAgenciesRequest>
    {
        public V1GetTopRentalAgenciesRequestValidator()
        {
            RuleFor(x => x.ApartmentFeatures)
                .Must(x => string.IsNullOrEmpty(x) || Enum.TryParse(x, true, out ApartmentFeatures _))
                .WithMessage($"ApartmentFeatures has invalid or empty value. Possible values are: {string.Join(",", Enum.GetValues<ApartmentFeatures>())}");

            RuleFor(x => x.Take)
                .Must(x => x.HasValue == false || x.Value > 0);
        }
    }
}