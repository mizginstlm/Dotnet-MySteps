using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetSteps.Dtos.Match
{
    public class MatchRequestDto
    {
        public List<Guid> CharacterIds { get; set; } = new List<Guid>();
    }
}