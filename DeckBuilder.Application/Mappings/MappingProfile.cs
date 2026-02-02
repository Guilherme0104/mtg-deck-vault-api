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
    }
}
