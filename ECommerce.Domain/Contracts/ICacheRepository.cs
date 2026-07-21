using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts
{
    public interface ICacheRepository
    {
        Task<string?> GetAsync(string CacheKey, CancellationToken ct = default);
        Task SetAsync(string CacheKey , string CacheValue, TimeSpan timeToLive , CancellationToken ct = default);


    }
}
