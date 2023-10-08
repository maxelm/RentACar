using RentACar.Domain.Entities;

namespace RentACar.Application.Contracts.Persistence
{
	public interface ICarRentalRepository : IAsyncRepository<CarRental>
	{
	}
}
