using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.IdentityDtos
{
    public class LoginDto
    {
        [EmailAddress] // Ensures the email is in a valid format @,.com w kda 3lshan asln el talb ywsl ll server
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
