using ECommerce.Application.Common;
using ECommerce.Application.DTO_s.ProductDto_s;
using ECommerce.Application.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Contracts
{
    public interface IProductServices
    {
        Task<Result<IReadOnlyList<ProductDto>>> GetAllProductsAsync(ProductQueryParams queryParams, CancellationToken ct = default);
        Task<Result<IReadOnlyList<BrandDto>>> GetAllProductBrandsAsync(CancellationToken ct = default);
        Task<Result<IReadOnlyList<TypeDto>>> GetAllProductTypesAsync(CancellationToken ct = default);
        Task<Result<ProductDto>> GetByIdAsync(int id, CancellationToken ct = default);
    }
}
