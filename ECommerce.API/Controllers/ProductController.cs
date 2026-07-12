using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO_s.ProductDto_s;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductServices productServices) : ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllProducts(CancellationToken ct)
        {
            var Products = await productServices.GetAllProductsAsync();
            var Result = ToActionResult(Products);
            return Result;
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllBrands(CancellationToken ct)
        {
            var Brands = await productServices.GetAllProductBrandsAsync();
            var Result = ToActionResult(Brands);
            return Result;
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypes(CancellationToken ct)
        {
            var types = await productServices.GetAllProductTypesAsync();
            var Result = ToActionResult(types);
            return Result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id ,CancellationToken ct)
        {
            var product = await productServices.GetByIdAsync(id);
            var Result = ToActionResult(product);
            return Result;
        }
    }
}
