using AutoMapper;
using DomainLayer.Models.IdentityModule;
using Shared.DataTransferObjects.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class AddressProfile:Profile
    {
        public AddressProfile()
        {
            CreateMap<Address,AddressDto>().ReverseMap();
        }
    }
}
