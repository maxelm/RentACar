using MediatR;

namespace RentACar.Application.Features.CarRentals.Commands.ReturnCarRental
{
	public class ReturnCarRentalCommand : IRequest<ReturnCarRentalDto>
	{
        public Guid BookingNumber { get; set; }
        public DateTime ReturnDate { get; set; }
        public int Mileage { get; set; }
    }
}
