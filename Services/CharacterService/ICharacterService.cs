using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetSteps.Dtos.Character;
using Microsoft.AspNetCore.Mvc;

namespace DotnetSteps.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(Guid id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(Guid id, UpdateCharacterDto updatedCharacter);
        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(Guid id);
        Task<ServiceResponse<GetCharacterDto>> AddCharacterAbility(AddCharacterAbilityDto newCharacterAbility);

    }
}