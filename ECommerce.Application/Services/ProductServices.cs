using AutoMapper;
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO_s.ProductDto_s;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class ProductServices:IProductServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductServices(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllProductBrandsAsync(CancellationToken ct = default)
        {
            var Brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(ct);
            var mappedBrands = mapper.Map<IReadOnlyList<ProductBrand>, IReadOnlyList<BrandDto>>(Brands);
            return Result<IReadOnlyList<BrandDto>>.ok(mappedBrands);
        }

        public async Task<Result<IReadOnlyList<ProductDto>>> GetAllProductsAsync(CancellationToken ct = default)
        {
            var Products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(ct);
            var mappedProducts = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(Products);
            return Result<IReadOnlyList<ProductDto>>.ok(mappedProducts);
        }

        public async Task<Result<IReadOnlyList      <TypeDto>>> GetAllProductTypesAsync(CancellationToken ct = default)
        {
            var Types = await unitOfWork.GetRepository<ProductsType, int>().GetAllAsync(ct);
            var mappedTypes = mapper.Map<IReadOnlyList<ProductsType>, IReadOnlyList<TypeDto>>(Types);
            return Result<IReadOnlyList<TypeDto>>.ok(mappedTypes);
        }

        public async Task<Result<ProductDto>> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var Product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(id, ct);

            if (Product is null) return Result<ProductDto>.Fail(Error.NotFound("Product.NotFound", "Product with Id: {id} is not found"));

            var mappedProduct = mapper.Map<Product, ProductDto>(Product);

            return Result<ProductDto>.ok(mappedProduct);

        }
    }
}
