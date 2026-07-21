using ECommerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDatabase _database;

        public CacheRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }
        public async Task<string?> GetAsync(string CacheKey, CancellationToken ct = default)
        {
            var value = await _database.StringGetAsync(CacheKey);
            return value.IsNullOrEmpty ? null : value.ToString();
        }

        public async Task SetAsync(string CacheKey, string CacheValue, TimeSpan timeToLive, CancellationToken ct = default)
        {
            await _database.StringSetAsync(CacheKey, CacheValue, timeToLive);
        }
    }
}
