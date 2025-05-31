using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")] //BaseUrl/api/product
    public class ProductController(IServiceManager _serviceManager):ControllerBase
    {
        //Get All Products 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products =await _serviceManager.productService.GetAllProductsAsync();
            return Ok(products);    
        }
        //Get product By Id 
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id) 
        {
            var product =await  _serviceManager.productService.GetProductByIdAsync(id);
            return Ok(product);
        }
        //Get all Types 
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            var types =await  _serviceManager.productService.GetAllTypesAsync();
            return Ok(types);
        }
        //Get all Brands
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands =await  _serviceManager.productService.GetAllBrandsAsync();
            return Ok(brands);
        }

    }
}
