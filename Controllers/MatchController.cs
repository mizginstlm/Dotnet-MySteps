using DotnetSteps.Dtos.Match;
using DotnetSteps.Services.MatchService;
using Microsoft.AspNetCore.Mvc;

namespace DotnetSteps.Controllers;

[ApiController]
[Route("[controller]")]
public class MatchController : ControllerBase
{
    private readonly IMatchService _MatchService;
    public MatchController(IMatchService MatchService)
    {
        _MatchService = MatchService;
    }

    [HttpPost("Weapon")]
    public async Task<ActionResult<ServiceResponse<AttackResultDto>>> WeaponAttack(PowerAttackDto request)
    {
        return Ok(await _MatchService.PowerAttack(request));
    }

    [HttpPost("Ability")]
    public async Task<ActionResult<ServiceResponse<AttackResultDto>>> AbilityAttack(AbilityAttackDto request)
    {
        return Ok(await _MatchService.AbilityAttack(request));
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<MatchResultDto>>> Match(MatchRequestDto request)
    {
        return Ok(await _MatchService.Match(request));
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<HighscoreDto>>>> GetHighscore()
    {
        return Ok(await _MatchService.GetHighscore());
    }
}