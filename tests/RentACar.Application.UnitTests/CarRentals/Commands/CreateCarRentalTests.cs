using AutoMapper;
using FluentAssertions;
using Moq;
using RentACar.Application.Contracts.Persistence;
using RentACar.Application.Features.CarRentals.Commands.CreateCarRental;
using RentACar.Application.Profiles;
using RentACar.Application.UnitTests.Mocks;
using RentACar.Domain.Enums;

namespace RentACar.Application.UnitTests.CarRentals.Commands
{
	public class CreateCarRentalTests
	{
		private readonly Mock<ICarRentalRepository> _mockCarRentalRepository;
		private readonly IMapper _mapper;

		public CreateCarRentalTests()
		{
			_mockCarRentalRepository = RepositoryMocks.GetCarRentalRepository();
			var configuration = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<MappingProfile>();
			});

			_mapper = configuration.CreateMapper();
		}


		[Fact]
		public async Task Should_Add_To_Rentals_Repository()
		{
			// Arrange
			var handler = new CreateCarRentalCommandHandler(_mockCarRentalRepository.Object, _mapper);
			var command = new CreateCarRentalCommand
			{
				RegistrationNumber = "ABC123",
				SocialSecurityNumber = "123",
				StartDate = DateTime.Now,
				Mileage = 1000,
				CarCategoryType = CarCategoryType.Small
			};

			// Act
			var response = await handler.Handle(command, It.IsAny<CancellationToken>());

			// Assert
			var carRentals = await _mockCarRentalRepository.Object.GetAllAsync();
			carRentals.Should().HaveCount(4);
			response.Should().NotBeNull();
		}
	}
}
