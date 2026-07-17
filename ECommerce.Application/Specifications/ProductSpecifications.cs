using ECommerce.Application.Params;
using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Specifications
{
    public class ProductSpecifications:BaseSpecifications<Product,int>
    {
        public ProductSpecifications(ProductQueryParams queryParams) 
            :base(p => (!queryParams.brandId.HasValue || p.BrandId == queryParams.brandId) 
            && (!queryParams.typeId.HasValue || p.TypeId == queryParams.typeId)
            && (string.IsNullOrEmpty(queryParams.SearchValue) || p.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);

            switch (queryParams.sort)
            {
                case ProductSortingOptions.NameAsc: AddOrderBy(p  => p.Name); break;
                case ProductSortingOptions.NameDesc: AddOrderByDesc(p  => p.Name); break;
                case ProductSortingOptions.PriceAsc: AddOrderBy(p  => p.Price); break;
                case ProductSortingOptions.PriceDesc: AddOrderByDesc(p  => p.Price); break;
                  _  : break;

            }
            ApplyPagination(queryParams.PageSize,queryParams.PageIndex);
        }

        public ProductSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);
        }
    }
}
