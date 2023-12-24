using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetSteps.Dtos.Character;
using DotnetSteps.Dtos.Power;

namespace DotnetSteps.Services.PowerService
{
    public interface IPowerService
    {
        Task<ServiceResponse<GetCharacterDto>> AddPower(AddPowerDto newPower);
    }
}