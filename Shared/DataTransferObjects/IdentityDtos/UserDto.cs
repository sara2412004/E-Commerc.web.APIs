using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.IdentityDtos
{
    //da ely hyt3mlo Return ll user lma a3ml login aw register 
    public class UserDto
    {
       // [EmailAddress] malosh lazma 3lshan da mn el server ll user[Output DTO]
        public string Email { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}
