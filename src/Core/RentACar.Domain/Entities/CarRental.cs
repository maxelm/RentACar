using RentACar.Domain.Enums;

namespace RentACar.Domain.Entities
{
	public class CarRental
	{
		public Guid Id { get; set; }
        public string RegistrationNumber { get; set; }
        public CarCategoryType CarCategoryType { get; set; }
        public string SocialSecurityNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int InitialMileage { get; set; }
        public int? FinalMileage { get; set; }
        public decimal? AmountDue { get; private set; }
        public decimal DailyFee { get; set; }
        public decimal MileageFee { get; set; }

        public void CalculateAmountDue()
		{
            if (!ReturnDate.HasValue || !FinalMileage.HasValue)
                throw new InvalidOperationException();

			var mileage = FinalMileage.Value - InitialMileage;
            var totalNumberOfDays = (int)Math.Ceiling((ReturnDate.Value - StartDate).TotalDays);

			AmountDue = CarCategoryType switch
            {
                CarCategoryType.Small => DailyFee * totalNumberOfDays,
                CarCategoryType.Combi => DailyFee * totalNumberOfDays * 1.3M + MileageFee * mileage,
                CarCategoryType.Truck => DailyFee * totalNumberOfDays * 1.5M + MileageFee * mileage * 1.5M,
                _ => throw new NotImplementedException()
            };
		}
	}
}
