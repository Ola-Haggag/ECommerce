using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Contracts
{
    public interface ICacheService
    {
        Task<string?> GetAsync(string cacheKey, CancellationToken ct = default);
        Task SetAsync(string cacheKey, object cacheValue, TimeSpan timeToLive ,CancellationToken ct = default);
    }
}
