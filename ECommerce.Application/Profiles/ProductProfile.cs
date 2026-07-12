using AutoMapper;
using ECommerce.Application.DTO_s.ProductDto_s;
using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Profiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductsType, TypeDto>();

            
            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.BrandName,opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dist => dist.TypeName,opt => opt.MapFrom(src => src.Type.Name));
        }
    }
}
