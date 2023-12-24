using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetSteps.Models
{
    public class Power
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; }
        public Guid CharacterId { get; set; }
        public Character? Character { get; set; }

    }
}