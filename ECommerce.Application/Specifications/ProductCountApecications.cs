using ECommerce.Application.Params;
using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Specifications
{
    public class ProductCountApecications:BaseSpecifications<Product,int>
    {
        public ProductCountApecications(ProductQueryParams queryParams)
      : base(p => (!queryParams.brandId.HasValue || p.BrandId == queryParams.brandId)
      && (!queryParams.typeId.HasValue || p.TypeId == queryParams.typeId)
      && (string.IsNullOrEmpty(queryParams.SearchValue) || p.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {

        }
    }
}
