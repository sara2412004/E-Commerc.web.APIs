using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager, IConfiguration _configuration,IMapper _mapper) : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            //1...........check if Email Exists
            var User = await _userManager.FindByEmailAsync(loginDto.Email)?? throw new UserNotFoundException(loginDto.Email);
            //2.check if password is correct
            var IsPasswordVaild = await _userManager.CheckPasswordAsync(User, loginDto.Password);
            if (IsPasswordVaild)
            {
                //3.generate token(Return UserDto)
                return new UserDto()
                {
                    DisplayName=User.DisplayName,
                    Email=User.Email,
                    Token =await CreateTokenAsync(User)

                };
            }
            else
            {
                // you are not authorized enk td5ol 3ndy 
                throw new UnauthorizedException();
            }
        }
        public async  Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            //1. mapping registerDto =App;icationUser
            var User = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                 Email = registerDto.Email,
                 PhoneNumber=registerDto.PhoneNumber,
                 UserName = registerDto.UserName,

            };
            //2. create User ely mn mo3 ApplicationUser
            var Result= await _userManager.CreateAsync(User,registerDto.Password);// password to Hashing
            //Result have 2 props succeeded,Errors
            if(Result.Succeeded)
            {
                //3.Return UserDto
                 return   new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = await CreateTokenAsync(User)

                 };
            }
            else
            {
               var Errors=Result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);

            }

        }
        public async Task<bool> CheckEmailAsync(string email)
        { 
            //check if email exists in DB
            var User = await _userManager.FindByEmailAsync(email);
            return User is not null;


        }

        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            var User=await _userManager.FindByEmailAsync(email);
            if (User is null) throw new UserNotFoundException(email);
            return new UserDto()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await CreateTokenAsync(User)
            };
        }
        public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
        {
           var User=await _userManager.Users.Include(U=>U.Address)
                                            .FirstOrDefaultAsync(U => U.Email==email)?? throw new UserNotFoundException(email);
            if (User.Address is not null) 
                return _mapper.Map<Address, AddressDto>(User.Address);

            else
                throw new AddressNotFoundException(User.UserName);  

        }

        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto)
        {
            var User = _userManager.Users.Include(U => U.Address)
                                            .FirstOrDefault(U => U.Email == email) ?? throw new UserNotFoundException(email);
           
            if (User.Address is null)
            {
                User.Address = _mapper.Map<AddressDto, Address>(addressDto);
            }
            else
            {
                //update the existing address
                User.Address.FirstName = addressDto.FirstName;
                User.Address.LastName = addressDto.LastName;
                User.Address.Street = addressDto.Street;
                User.Address.City = addressDto.City;
                User.Address.Country = addressDto.Country;
            }
            await _userManager.UpdateAsync(User);
            return _mapper.Map<Address, AddressDto>(User.Address);


        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            //1.create claims
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
            };
            //2.get user roles & add them to claims
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
            // el SecretKey ,issuer , audience in appsetting
            var SecretKey = _configuration.GetSection("JwtOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            //the algorithm ely hst5dmha fe encryption 
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            //3.generate token
            var Token = new JwtSecurityToken
                (
                //issuer men el server elly 3ml el token w create JWT
                issuer: _configuration.GetSection("JwtOptions")["Issuer"],
                audience: _configuration.GetSection("JwtOptions")["Audience"],
                claims: Claims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: Creds
                );  
            //convert el Token to string 
            return  new JwtSecurityTokenHandler().WriteToken(Token);

        }
    }
}
