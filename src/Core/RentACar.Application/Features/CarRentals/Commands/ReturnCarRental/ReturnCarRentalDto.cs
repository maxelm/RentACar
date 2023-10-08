using RentACar.Domain.Enums;

namespace RentACar.Application.Features.CarRentals.Commands.ReturnCarRental
{
	public class ReturnCarRentalDto
	{
		public Guid BookingNumber { get; set; }
		public string RegistrationNumber { get; set; } = string.Empty;
		public CarCategoryType CarCategoryType { get; set; }
		public string SocialSecurityNumber { get; set; } = string.Empty;
		public DateTime StartDate { get; set; }
		public DateTime ReturnDate { get; set; }
		public int InitialMileage { get; set; }
		public int FinalMileage { get; set; }
		public decimal DailyFee { get; set; }
		public decimal MileageFee { get; set; }
		public decimal AmountDue { get; set; }
	}
}
