using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IproductService
    {
        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {

            var Repo = _unitOfWork.GetRepository<Product,int>();
            var Specifications =new ProductWithBrandAndTypeSpecifications(queryParams); //Specifications ,bb3t ll Ctro ely hy3mlo 
            var Products =await Repo.GetAllAsync(Specifications);
            var Data=_mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
            var ProductCount = Data.Count();
            var CountSpec = new ProductCountSpecifications(queryParams);
            var TotalCount = await Repo.CountAsync(CountSpec);
            return new PaginatedResult<ProductDto>(queryParams.PageIndex,ProductCount,TotalCount,Data);
        }
        
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var Specifications =new ProductWithBrandAndTypeSpecifications(id); //Specifications
            var product = await Repo.GetByIdAsync(Specifications);
            var productDto =_mapper.Map<ProductDto>(product);   
            return productDto;  
        }
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductBrand,int>();
            var Brands=await  Repo.GetAllAsync(); // hena gaialy IEnumerable<ProductBrand>> f h3ml mapping 
            var BrandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
            return BrandsDto;
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductType, int>();
            var Types = await Repo.GetAllAsync();
            var TypesDto = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
            return TypesDto;
        }


      
    }
}
