using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetSteps.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace DotnetSteps.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{


    private readonly ICharacterService _CharacterService;

    public CharacterController(ICharacterService characterService)
    {
        _CharacterService = characterService;
    }
    [HttpGet("GetAll")]
    public ActionResult<List<Character>> Get()
    {
        return Ok(_CharacterService.GetAllCharacters());
    }

    [HttpGet("{id}")]
    public ActionResult<Character> GetSingle(int id)
    {
        return Ok(_CharacterService.GetCharacterById(id));
    }

    [HttpPost]
    public ActionResult<List<Character>> AddCharacter(Character newCharacter)
    {

        return Ok(_CharacterService.AddCharacter(newCharacter));
    }

}