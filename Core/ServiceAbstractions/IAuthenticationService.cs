using Shared.DataTransferObjects.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        //1.login bta5od email ,password w return UserDto 
        public Task<UserDto> LoginAsync(LoginDto loginDto);
        //2.register take email ,password ,displayname, username, phoneNumber  
        //w return UserDto [email , displayname , token]
        public Task<UserDto> RegisterAsync(RegisterDto registerDto);

        //3.check if email exists[take Email Then Return boolean To Client  ]
        public Task<bool> CheckEmailAsync(string email);

        //4.Take Email Then Return Address of Current Logged in User 
        public Task<AddressDto> GetCurrentUserAddressAsync(string email);
        //5.Take Email,AddressDto Then Update The Address of Current Logged in User And Return The Updated AddressDto
        public Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto);
        //6.Take Email Then Return Token , Email and DisplayName 
        public Task<UserDto> GetCurrentUserAsync(string email);

    }
}
