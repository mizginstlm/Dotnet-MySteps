using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetSteps.Dtos.Character
{
    public class AddCharacterAbilityDto
    {
        public Guid CharacterId { get; set; }
        public int AbilityId { get; set; }
    }
}