using ECommerce.Application.Contracts;
using ECommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace ECommerce.API.Attributes
{
    public class RedisCacheAttribute:ActionFilterAttribute
    {
        private readonly int _durationInSec;

        public RedisCacheAttribute(int DurationInSrc)
        {
            _durationInSec = DurationInSrc;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Get Cache Service From DI Container
            var CacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var CachedKey = CreateCacheKey(context.HttpContext.Request);
            //Check if Cached Data Exsists
            var Cached = await CacheService.GetAsync(CachedKey);
            //If Exsists, Return Cached Data and Skip Exceution of EndPoint
            if (!string.IsNullOrEmpty(Cached))
            {
                context.Result = new ContentResult()
                {
                    Content = Cached,
                    ContentType = "Application/json",
                    StatusCode = StatusCodes.Status200OK,
                };
                return;
            }
            //If Not Exsist, Execute EndPoint , and Store The Result in Cache if 200 Ok Response
            var Executed = await next.Invoke();
            if (Executed.Result is OkObjectResult { Value: not null } ok)
                await CacheService.SetAsync(CachedKey, ok.Value, TimeSpan.FromSeconds(_durationInSec));
            return ;
        }

        private static string CreateCacheKey(HttpRequest request)
        {
            //Path
            //api/Product
            //api/Product?brandId=10
            //api/Product?TypeId=3


            var Key = new StringBuilder();
            Key.Append(request.Path).Append("?");

            foreach(var (k,v) in request.Query.OrderBy(Q=>Q.Key))
            {
                Key.Append(k).Append("=").Append(v).Append("&");
            }
            return Key.ToString();
        }
    }
}
