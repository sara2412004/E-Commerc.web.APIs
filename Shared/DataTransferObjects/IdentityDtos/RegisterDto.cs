using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.IdentityDtos
{
    public class RegisterDto
    {
        [EmailAddress] 
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public string UserName { get; set; } = default!;
        [Phone] // Ensures the phone number is in a valid format 3lshan ana m5liah string bs 
        public string PhoneNumber { get; set; } = default!;

    }
}
