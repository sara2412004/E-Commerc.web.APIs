using DomainLayer.Models.ProductModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class ProductCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductCountSpecifications(ProductQueryParams queryParams) 
            : base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId) && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId))
        {
        }
    }
}
