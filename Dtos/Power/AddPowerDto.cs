using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetSteps.Dtos.Power
{
    public class AddPowerDto
    {

        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; }
        public Guid CharacterId { get; set; }
    }
}