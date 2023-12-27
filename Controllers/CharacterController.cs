using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetSteps.Dtos.Character;
using DotnetSteps.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetSteps.Controllers;
[Authorize]//ı used middleware and some configs about jwtbearer to use this
[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{


    private readonly ICharacterService _characterService;

    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    // [AllowAnonymous]//everyone can see doesnt have to authorze
    //http://localhost:5042/api/Character/GetAll?filterOn=Name&filterQuery=Track
    [HttpGet("GetAll")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get(string? filterOn, string? filterQuery, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
    {
        return Ok(await _characterService.GetAllCharacters(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(Guid id)
    {
        var response = await _characterService.GetCharacterById(id);
        if (response.Data is null)
        {
            return NotFound(response);
        }
        return Ok(response);

    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
    {

        if (ModelState.IsValid)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }
        return BadRequest(ModelState);
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> UpdateCharacter(Guid id, UpdateCharacterDto updatedCharacter)
    {

        if (ModelState.IsValid)
        {
            var response = await _characterService.UpdateCharacter(id, updatedCharacter);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        return BadRequest(ModelState);

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> DeleteCharacter(Guid id)
    {
        var response = await _characterService.DeleteCharacter(id);
        if (response.Data is null)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpPost("Ability")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddCharacterAbility(
           AddCharacterAbilityDto newCharacterAbility)
    {
        return Ok(await _characterService.AddCharacterAbility(newCharacterAbility));
    }

}