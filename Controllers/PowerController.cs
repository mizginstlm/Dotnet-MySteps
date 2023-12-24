using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetSteps.Dtos.Character;
using DotnetSteps.Dtos.Power;
using DotnetSteps.Services.PowerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetSteps.Controllers
{

    [Authorize]//ı used middleware and some configs about jwtbearer to use this
    [ApiController]
    [Route("api/[controller]")]
    public class PowerController : ControllerBase
    {
        private readonly IPowerService _powerService;
        public PowerController(IPowerService powerService)
        {
            _powerService = powerService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddPower(AddPowerDto newPower)
        {
            return Ok(await _powerService.AddPower(newPower));
        }
    }

}