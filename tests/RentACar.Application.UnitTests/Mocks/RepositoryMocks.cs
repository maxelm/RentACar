using Moq;
using RentACar.Application.Contracts.Persistence;
using RentACar.Domain.Entities;

namespace RentACar.Application.UnitTests.Mocks
{
	public class RepositoryMocks
	{
		public static Mock<ICarRentalRepository> GetCarRentalRepository()
		{
			var carRentals = new List<CarRental>
			{
				new CarRental(),
				new CarRental(),
				new CarRental(),
			};
			
			var mockCarRentalRepository = new Mock<ICarRentalRepository>();

			mockCarRentalRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(carRentals);
			mockCarRentalRepository.Setup(repo => repo.AddAsync(It.IsAny<CarRental>())).ReturnsAsync(
				(CarRental category) =>
				{
					carRentals.Add(category);
					return category;
				});

			return mockCarRentalRepository;
		}
	}
}
