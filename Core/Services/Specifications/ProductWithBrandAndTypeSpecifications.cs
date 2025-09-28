using DomainLayer.Models.ProductModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications:BaseSpecifications<Product,int>
    {
        //Get All
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams) 
            :base(p=>(!queryParams.BrandId.HasValue||p.BrandId== queryParams.BrandId) &&(!queryParams.TypeId.HasValue||p.TypeId== queryParams.TypeId))
        {
            AddIncludeExpression(P => P.ProductBrand);
            AddIncludeExpression(P=>P.ProductType);


            switch (queryParams.sortingOptions) 
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;

                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;

                case ProductSortingOptions.PriceAsc: 
                    AddOrderBy(p => p.Price);
                    break;

                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;

                default:
                    // htf2 m3 el frontend
                    break;
            
            
            }
            //Pagination
            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
            
        }
        //Get by ID
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddIncludeExpression(P => P.ProductBrand); 
            AddIncludeExpression(P => P.ProductType);

        }
       




    }
}
