using DotnetSteps.Dtos.Character;
using DotnetSteps.Dtos.Power;
using DotnetSteps.Dtos.Ability;
using DotnetSteps.Dtos.Match;

namespace DotnetSteps;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Character, GetCharacterDto>();
        CreateMap<AddCharacterDto, Character>();
        CreateMap<Power, GetPowerDto>();
        CreateMap<Ability, GetAbilityDto>();
        CreateMap<Character, HighscoreDto>();

    }
}