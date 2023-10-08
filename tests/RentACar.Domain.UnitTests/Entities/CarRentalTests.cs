using FluentAssertions;
using RentACar.Domain.Entities;
using RentACar.Domain.Enums;

namespace RentACar.Domain.UnitTests.Entities
{

	public class CarRentalTests
	{
		[Theory]
		[InlineData(CarCategoryType.Small, 200)]
		[InlineData(CarCategoryType.Combi, 1260)]
		[InlineData(CarCategoryType.Truck, 1800)]
		public void CalculateAmountDue_Should_Calculate_Correct_Price(CarCategoryType carCategoryType, decimal expectedAmountDue)
		{
			// Arrange
			var startDate = DateTime.Now;
			var carRental = new CarRental
			{
				CarCategoryType = carCategoryType,
				InitialMileage = 0,
				FinalMileage = 1000,
				StartDate = startDate,
				ReturnDate = startDate.AddDays(2),
				MileageFee = 1,
				DailyFee = 100
			};

			// Act
			carRental.CalculateAmountDue();

			// Assert
			carRental.AmountDue.HasValue.Should().BeTrue();
			carRental.AmountDue.Value.Should().Be(expectedAmountDue);
		}

		[Fact]
		public void CalculateAmountDue_Should_Could_Started_Days_As_Full_Days()
		{
			// Arrange
			var startDate = DateTime.Now;
			var returnDate = startDate.AddDays(2).AddHours(1);
			var carRental = new CarRental
			{
				CarCategoryType = CarCategoryType.Small,
				InitialMileage = 0,
				FinalMileage = 1000,
				StartDate = startDate,
				ReturnDate = returnDate,
				MileageFee = 1,
				DailyFee = 100
			};

			// Act
			carRental.CalculateAmountDue();

			// Assert
			carRental.AmountDue.HasValue.Should().BeTrue();
			carRental.AmountDue.Value.Should().Be(300);
		}
	}
}
