using AutoMapper;
using CsharpClicker.Domain;
using CsharpClicker.UseCases.GetBoosts;
using CsharpClicker.UseCases.GetCurrentUser;

namespace CsharpClicker.UseCases;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Boost, BoostDto>();
        CreateMap<UserBoost, UserBoostDto>();
        CreateMap<ApplicationUser, UserDto>();
    }
}
