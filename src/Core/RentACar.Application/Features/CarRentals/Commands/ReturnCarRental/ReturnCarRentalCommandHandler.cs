using AutoMapper;
using MediatR;
using RentACar.Application.Contracts.Persistence;

namespace RentACar.Application.Features.CarRentals.Commands.ReturnCarRental
{
	public class ReturnCarRentalCommandHandler : IRequestHandler<ReturnCarRentalCommand, ReturnCarRentalDto>
	{
		private readonly ICarRentalRepository _carRentalRepository;
		private readonly IMapper _mapper;

		public ReturnCarRentalCommandHandler(ICarRentalRepository carRentalRepository, IMapper mapper)
		{
			_carRentalRepository = carRentalRepository;
			_mapper = mapper;
		}

		public async Task<ReturnCarRentalDto> Handle(ReturnCarRentalCommand request, CancellationToken cancellationToken)
		{
			var carRental = await _carRentalRepository.GetByIdAsync(request.BookingNumber) ?? throw new Exception();

			carRental.ReturnDate = request.ReturnDate;
			carRental.FinalMileage = request.Mileage;
			carRental.CalculateAmountDue();

			await _carRentalRepository.UpdateAsync(carRental);

			return _mapper.Map<ReturnCarRentalDto>(carRental);
		}
	}
}
