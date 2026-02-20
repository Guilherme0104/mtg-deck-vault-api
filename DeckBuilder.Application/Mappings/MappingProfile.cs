using AutoMapper;
using DeckBuilder.Domain.Entities;
using DeckBuilder.Application.DTOs;

namespace DeckBuilder.Application.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<CreateDeckRequest, Deck>();
		CreateMap<CreateUserRequest, User>();
		CreateMap<UpdateDeckRequest, Deck>();
        CreateMap<UpdateUserRequest, User>();
        CreateMap<CreateReviewRequest, Review>();
        CreateMap<User, UserResponseDTO>();
        CreateMap<Deck, DeckShortResponseDTO>();
        CreateMap<Review, ReviewResponseDTO>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
    }
}
