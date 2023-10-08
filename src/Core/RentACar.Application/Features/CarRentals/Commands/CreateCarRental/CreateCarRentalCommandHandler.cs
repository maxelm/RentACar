using AutoMapper;
using MediatR;
using RentACar.Application.Contracts.Persistence;
using RentACar.Domain.Entities;

namespace RentACar.Application.Features.CarRentals.Commands.CreateCarRental
{
	public class CreateCarRentalCommandHandler : IRequestHandler<CreateCarRentalCommand, CreateCarRentalDto>
	{
		private readonly ICarRentalRepository _carRentalRepository;
		private readonly IMapper _mapper;

		public CreateCarRentalCommandHandler(ICarRentalRepository carRentalRepository, IMapper mapper)
		{
			_carRentalRepository = carRentalRepository;
			_mapper = mapper;
		}

		public async Task<CreateCarRentalDto> Handle(CreateCarRentalCommand request, CancellationToken cancellationToken)
		{
			var carRental = _mapper.Map<CarRental>(request);
			carRental.DailyFee = Constants.CarRentalFees.DailyFee;
			carRental.MileageFee = Constants.CarRentalFees.MileageFee;

			carRental = await _carRentalRepository.AddAsync(carRental);

			return _mapper.Map<CreateCarRentalDto>(carRental);
		}
	}
}
