using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetSteps.Dtos.User
{
    public class UserRegisterDto
    {

        [Required(ErrorMessage = "Must have a name.")][StringLength(50, ErrorMessage = "PLease write with a valid length.")] public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Must have a name.")][StringLength(50, MinimumLength = 8, ErrorMessage = "PLease write with a valid length.")] public string Password { get; set; } = string.Empty;

    }
}