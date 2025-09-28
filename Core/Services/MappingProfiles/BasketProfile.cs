using AutoMapper;
using DomainLayer.Models.BasketModule;
using DomainLayer.Models.IdentityModule;
using Shared.DataTransferObjects.BasketDtos;
using Shared.DataTransferObjects.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class BasketProfile: Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketDto,CustomerBasket>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

        }

    }
}
