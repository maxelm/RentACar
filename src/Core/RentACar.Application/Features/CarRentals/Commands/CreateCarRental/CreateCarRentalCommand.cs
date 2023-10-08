using MediatR;
using RentACar.Domain.Enums;

namespace RentACar.Application.Features.CarRentals.Commands.CreateCarRental
{
	public class CreateCarRentalCommand : IRequest<CreateCarRentalDto>
	{
		public string SocialSecurityNumber { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
		public CarCategoryType CarCategoryType { get; set; }
		public DateTime StartDate { get; set; }
		public int Mileage { get; set; }
	}
}
