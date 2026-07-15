using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Params
{
    public class ProductQueryParams
    {
        public int? brandId {  get; set; }
        public int? typeId { get; set; }
        public string? SearchValue { get; set; }

        public ProductSortingOptions sort { get; set; }
    }
}
