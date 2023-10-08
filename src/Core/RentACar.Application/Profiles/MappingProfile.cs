using AutoMapper;
using RentACar.Application.Features.CarRentals.Commands.CreateCarRental;
using RentACar.Application.Features.CarRentals.Commands.ReturnCarRental;
using RentACar.Domain.Entities;

namespace RentACar.Application.Profiles
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			// CreateCarRental
			CreateMap<CreateCarRentalCommand, CarRental>()
				.ForMember(dest => dest.InitialMileage, opt => opt.MapFrom(src => src.Mileage))
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.ReturnDate, opt => opt.Ignore())
				.ForMember(dest => dest.FinalMileage, opt => opt.Ignore())
				.ForMember(dest => dest.AmountDue, opt => opt.Ignore())
				.ForMember(dest => dest.DailyFee, opt => opt.Ignore())
				.ForMember(dest => dest.MileageFee, opt => opt.Ignore());
			CreateMap<CarRental, CreateCarRentalDto>()
				.ForMember(dest => dest.BookingNumber, opt => opt.MapFrom(src => src.Id));

		// ReturnCarRental
		CreateMap<CarRental, ReturnCarRentalDto>()
				.ForMember(dest => dest.BookingNumber, opt => opt.MapFrom(src => src.Id));
		}
	}
}