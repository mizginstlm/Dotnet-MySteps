using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetSteps.Dtos.Match;

namespace DotnetSteps.Services.MatchService
{
    public interface IMatchService
    {
        Task<ServiceResponse<AttackResultDto>> PowerAttack(PowerAttackDto request);
        Task<ServiceResponse<AttackResultDto>> AbilityAttack(AbilityAttackDto request);
        Task<ServiceResponse<MatchResultDto>> Match(MatchRequestDto request);
        Task<ServiceResponse<List<HighscoreDto>>> GetHighscore();
    }
}