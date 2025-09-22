using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager) :ApiBaseController
    {
        //Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user=await _serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(user);

        }
        //Register
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
           var user=await _serviceManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(user);
        }
        //CheckEmail
        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> EmailExists([FromQuery] string email)
        {
            var Result = await _serviceManager.AuthenticationService.CheckEmailAsync(email);
            return Ok(Result);

        }
        //GetCurrentUser
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);  
            var user = await _serviceManager.AuthenticationService.GetCurrentUserAsync(email!);
            return Ok(user);
        }
        //GetCurrentUserAddress
        [Authorize]
        [HttpGet("GetCurrentUserAddress")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await _serviceManager.AuthenticationService.GetCurrentUserAddressAsync(email!);
            return Ok(address);
        }
        //UpdateCurrentUserAddress
        [Authorize]
        [HttpPut("UpdateCurrentUserAddress")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var updateAddress = await _serviceManager.AuthenticationService.UpdateCurrentUserAddressAsync(email!, addressDto);
            return Ok(updateAddress);
        }
    }
}
