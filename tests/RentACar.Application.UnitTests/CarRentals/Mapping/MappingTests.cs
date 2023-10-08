using AutoMapper;
using FluentAssertions;
using RentACar.Application.Features.CarRentals.Commands.CreateCarRental;
using RentACar.Application.Features.CarRentals.Commands.ReturnCarRental;
using RentACar.Application.Profiles;
using RentACar.Domain.Entities;
using RentACar.Domain.Enums;

namespace RentACar.Application.UnitTests.CarRentals.Mapping
{
	public class MappingTests
	{
		private readonly IMapper _mapper;
		private readonly MapperConfiguration _configuration;

		public MappingTests()
		{
			_configuration = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<MappingProfile>();
			});

			_mapper = _configuration.CreateMapper();
		}

		[Fact]
		public void ConfigurationTest()
		{
			_configuration.AssertConfigurationIsValid();
		}

		[Fact]
		public void CreateCarRentalCommand_To_CarRental()
		{
			// Arrange
			CreateCarRentalCommand command = new()
			{
				SocialSecurityNumber = "198907261234",
				CarCategoryType = CarCategoryType.Truck,
				Mileage = 10,
				RegistrationNumber = "ABC123",
				StartDate = DateTime.Now,
			};

			// Act
			CarRental carRental = _mapper.Map<CarRental>(command);

			// Assert
			carRental.Should().BeEquivalentTo(command, opt => opt.WithMapping("Mileage", "InitialMileage"));
		}

		[Fact]
		public void CarRental_To_CreateCarRentalDto()
		{
			// Arrange
			CarRental carRental = new()
			{
				Id = Guid.NewGuid(),
				RegistrationNumber = "ABC123",
				CarCategoryType = CarCategoryType.Truck,
				SocialSecurityNumber = "198907261234",
				StartDate = DateTime.Now,
				ReturnDate = DateTime.Now.AddDays(1),
				InitialMileage = 10,
				FinalMileage = 100,
				DailyFee = 100,
				MileageFee = 1
			};

			// Act
			CreateCarRentalDto dto = _mapper.Map<CreateCarRentalDto>(carRental);

			// Assert
			dto.Should().BeEquivalentTo(carRental, opt => opt
				.WithMapping("BookingNumber", "Id")
				.ExcludingMissingMembers());
		}

		[Fact]
		public void CarRental_To_ReturnCarRentalDto()
		{
			// Arrange
			CarRental carRental = new()
			{
				Id = Guid.NewGuid(),
				RegistrationNumber = "ABC123",
				CarCategoryType = CarCategoryType.Truck,
				SocialSecurityNumber = "198907261234",
				StartDate = DateTime.Now,
				ReturnDate = DateTime.Now.AddDays(1),
				InitialMileage = 10,
				FinalMileage = 100,
				DailyFee = 100,
				MileageFee = 1
			};
			carRental.CalculateAmountDue();

			// Act
			ReturnCarRentalDto dto = _mapper.Map<ReturnCarRentalDto>(carRental);

			// Assert
			dto.AmountDue.Should().BeGreaterThan(0);
			dto.Should().BeEquivalentTo(carRental, opt => opt
				.WithMapping("BookingNumber", "Id")
				.ExcludingMissingMembers());
		}
	}
}
