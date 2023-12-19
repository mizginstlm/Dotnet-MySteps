using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetSteps.Models
{
    public class User
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Must have a name.")][StringLength(50, ErrorMessage = "PLease write with a valid length.")] public string UserName { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];
        // public List<Character>? Characters { get; set; }

    }
}