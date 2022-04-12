using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using RealEstateAgency.Services.Implementations;
using RealEstateAgency.Services.Interfaces;
using RealEstateAgency.Services.Models;
using RealEstateAgency.Services.Tests.Bootstrap;
using Xunit;

namespace RealEstateAgency.Services.Tests
{
    public class RealEstateAgencyServiceTests
    {
        [Fact]
        public async Task CallGetTopRealEstateAgencies_ShouldReturnCorrectResponse()
        {
            //arrange
            var service = CompositionRoot.ServiceProvider.GetRequiredService<IRealEstateAgencyService>();
            var dto = new GetTopRealEstateAgenciesDto
            {
                Take = 3,
                ApartmentsFeature = ApartmentFeatures.Balcon | ApartmentFeatures.Dakterras | ApartmentFeatures.Tuin
            };

            //act
            var result = await service.GetTopRealEstateAgencies(dto);

            //assert
            result.IsSuccess.Should().BeTrue();
            var value = result.Value;

            value.RealEstatesInfo.Should().BeEquivalentTo(new GetTopRealEstateAgenciesResult.RealEstateInfo
            {
                Name = "MakelaarName1",
                AdvtsCount = 3
            }, new GetTopRealEstateAgenciesResult.RealEstateInfo
            {
                Name = "MakelaarName2",
                AdvtsCount = 2
            }, new GetTopRealEstateAgenciesResult.RealEstateInfo
            {
                Name = "MakelaarName3",
                AdvtsCount = 1
            });
        }

        [Fact]
        public async Task CallGetTopRealEstateAgencies_ShouldReturnCorrectResponseForTakeLessThanOverallNumberOfItems()
        {
            //arrange
            var service = CompositionRoot.ServiceProvider.GetRequiredService<IRealEstateAgencyService>();
            var dto = new GetTopRealEstateAgenciesDto
            {
                Take = 2,
                ApartmentsFeature = ApartmentFeatures.Balcon | ApartmentFeatures.Dakterras | ApartmentFeatures.Tuin
            };

            //act
            var result = await service.GetTopRealEstateAgencies(dto);

            //assert
            result.IsSuccess.Should().BeTrue();
            var value = result.Value;

            value.RealEstatesInfo.Should().BeEquivalentTo(new GetTopRealEstateAgenciesResult.RealEstateInfo
            {
                Name = "MakelaarName1",
                AdvtsCount = 3
            }, new GetTopRealEstateAgenciesResult.RealEstateInfo
            {
                Name = "MakelaarName2",
                AdvtsCount = 2
            });
        }

        [Fact]
        public void BuildGetRealEstatesDto_ShouldReturnCorrectDtoWithBalcon()
        {
            var getTopRealEstateAgenciesDto = new GetTopRealEstateAgenciesDto
            {
                ApartmentsFeature = ApartmentFeatures.Balcon
            };

            var getRealEstatesDto = RealEstateAgencyService.BuildGetRealEstatesDto(getTopRealEstateAgenciesDto);
            getRealEstatesDto.PageSize.Should().Be(200);
            getRealEstatesDto.WithBalcon.Should().BeTrue();
            getRealEstatesDto.WithDakterras.Should().BeFalse();
            getRealEstatesDto.WithTuin.Should().BeFalse();
        }
        
        [Fact]
        public void BuildGetRealEstatesDto_ShouldReturnCorrectDtoWithTuin()
        {
            var getTopRealEstateAgenciesDto = new GetTopRealEstateAgenciesDto
            {
                ApartmentsFeature = ApartmentFeatures.Tuin
            };

            var getRealEstatesDto = RealEstateAgencyService.BuildGetRealEstatesDto(getTopRealEstateAgenciesDto);
            getRealEstatesDto.PageSize.Should().Be(200);
            getRealEstatesDto.WithTuin.Should().BeTrue();
            getRealEstatesDto.WithDakterras.Should().BeFalse();
            getRealEstatesDto.WithBalcon.Should().BeFalse();
        }
        
        [Fact]
        public void BuildGetRealEstatesDto_ShouldReturnCorrectDtoWithDakterras()
        {
            var getTopRealEstateAgenciesDto = new GetTopRealEstateAgenciesDto
            {
                ApartmentsFeature = ApartmentFeatures.Dakterras
            };

            var getRealEstatesDto = RealEstateAgencyService.BuildGetRealEstatesDto(getTopRealEstateAgenciesDto);
            getRealEstatesDto.PageSize.Should().Be(200);
            getRealEstatesDto.WithTuin.Should().BeFalse();
            getRealEstatesDto.WithDakterras.Should().BeTrue();
            getRealEstatesDto.WithBalcon.Should().BeFalse();
        }
    }
}