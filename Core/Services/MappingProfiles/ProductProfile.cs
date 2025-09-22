using AutoMapper;
using DomainLayer.Models;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dis => dis.TypeName, options => options.MapFrom(src => src.ProductType.Name))
                .ForMember(dis => dis.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dis => dis.PictureUrl, options => options.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand,BrandDto>();
            CreateMap<ProductType,TypeDto>();   



        }





    }
}
