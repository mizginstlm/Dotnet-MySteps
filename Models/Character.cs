using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetSteps.Models;

public class Character
{


    public Guid Id { get; set; }
    [Required(ErrorMessage = "Must have a name.")][StringLength(50, ErrorMessage = "PLease write with a valid length.")] public string Name { get; set; } = "Ninja Samurai";
    [Range(1, 100, ErrorMessage = "HitPoints must be between 1-100.")] public int HitPoints { get; set; } = 100;
    [Range(1, 100, ErrorMessage = "Strength must be between 1-100.")] public int Strength { get; set; } = 10;
    [Range(1, 100, ErrorMessage = "Defense must be between 1-100.")] public int Defense { get; set; } = 10;
    [Range(1, 100, ErrorMessage = "Intelligence must be between 1-100.")] public int Intelligence { get; set; } = 10;
    public CharacterClass Class { get; set; } = CharacterClass.Knight;
    [Url][StringLength(100, ErrorMessage = "PLease write with a valid length")] public string? ImageUri { get; set; }

    public User? User { get; set; }
    public Power? Power { get; set; }

}