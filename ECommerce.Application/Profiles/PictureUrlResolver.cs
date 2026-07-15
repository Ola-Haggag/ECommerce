using AutoMapper;
using ECommerce.Application.DTO_s.ProductDto_s;
using ECommerce.Domain.Entities.Products;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ECommerce.Application.Profiles
{
    public class PictureUrlResolver(IOptions<UrlSettings> options) : IValueResolver<Product, ProductDto, string?>
    {
        private readonly UrlSettings _urlSettings = options.Value ;
        public string? Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl)) { return  null; }
           
            var baseUrl = _urlSettings.BaseUrl.TrimEnd('/');
            
            var Path = source.PictureUrl.TrimStart('/');

            return $"{baseUrl}/Files/{Path}";
        }
    }

    public class UrlSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
    }
}
