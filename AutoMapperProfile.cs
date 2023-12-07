using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetSteps.Dtos.Character;

namespace DotnetSteps
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();

        }
    }
}