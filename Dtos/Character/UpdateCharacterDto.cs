using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetSteps.Dtos.Character
{
    public class UpdateCharacterDto
    {
        public int Id { get; set; }
        [Required][StringLength(50)] public string Name { get; set; } = "Frodo";
        [Range(1, 100)] public int HitPoints { get; set; } = 100;
        [Range(1, 100)] public int Strength { get; set; } = 10;
        [Range(1, 100)] public int Defense { get; set; } = 10;
        [Range(1, 100)] public int Intelligence { get; set; } = 10;
        public CharacterClass Class { get; set; } = CharacterClass.Knight;
        [Url][StringLength(100)] public string? ImageUri { get; set; }
    }
}