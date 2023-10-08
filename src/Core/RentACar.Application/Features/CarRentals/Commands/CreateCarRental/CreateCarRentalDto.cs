using RentACar.Domain.Enums;

namespace RentACar.Application.Features.CarRentals.Commands.CreateCarRental
{
	public class CreateCarRentalDto
	{
		public Guid BookingNumber { get; set; }
		public string RegistrationNumber { get; set; }
		public CarCategoryType CarCategoryType { get; set; }
		public string SocialSecurityNumber { get; set; }
		public DateTime StartDate { get; set; }
		public int InitialMileage { get; set; }
		public decimal DailyFee { get; set; }
		public decimal MileageFee { get; set; }
	}
}
