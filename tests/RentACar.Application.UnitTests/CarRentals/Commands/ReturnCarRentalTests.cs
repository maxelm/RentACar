using AutoMapper;
using FluentAssertions;
using Moq;
using RentACar.Application.Contracts.Persistence;
using RentACar.Application.Features.CarRentals.Commands.ReturnCarRental;
using RentACar.Application.Profiles;
using RentACar.Application.UnitTests.Mocks;
using RentACar.Domain.Entities;
using RentACar.Domain.Enums;

namespace RentACar.Application.UnitTests.CarRentals.Commands
{
	public class ReturnCarRentalTests
	{
		private readonly Mock<ICarRentalRepository> _mockCarRentalRepository;
		private readonly IMapper _mapper;

		public ReturnCarRentalTests()
		{
			_mockCarRentalRepository = RepositoryMocks.GetCarRentalRepository();
			var configuration = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<MappingProfile>();
			});

			_mapper = configuration.CreateMapper();
		}

		[Fact]
		public async Task Should_Calculate_AmountDue()
		{
			// Arrange
			var activeCarRental = new CarRental
			{
				Id = Guid.Parse("B0788D2F-8003-43C1-92A4-EDC76A7C5DDE"),
				CarCategoryType = CarCategoryType.Small,
				RegistrationNumber = "ABC 123",
				SocialSecurityNumber = "19890726-1234",
				StartDate = DateTime.Parse("2023-10-08"),
				ReturnDate = null,
				InitialMileage = 1000,
				FinalMileage = null,
				DailyFee = 500,
				MileageFee = 10,
			};

			_mockCarRentalRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(activeCarRental);

			var handler = new ReturnCarRentalCommandHandler(_mockCarRentalRepository.Object, _mapper);
			var command = new ReturnCarRentalCommand
			{
				BookingNumber = Guid.NewGuid(),
				ReturnDate = DateTime.Parse("2023-10-15"),
				Mileage	= 1500
			};

			// Act
			var carRental = await handler.Handle(command, It.IsAny<CancellationToken>());

			// Assert
			carRental.AmountDue.Should().BeGreaterThan(0);
		}
	}
}
