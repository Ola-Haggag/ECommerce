using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Products;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Seeding
{
    public class CatalogDataSeeder(StoreDbContext dbContext, ILogger<CatalogDataSeeder> logger) : IDataSeeder
    {
        public async Task SeedAsync(CancellationToken ct = default)
        {
            try
            {
                var SeedPath = Path.Combine(AppContext.BaseDirectory, "DataSeed");
                await SeedIfEmptyAsync<ProductBrand>(SeedPath, "brands.json", ct);
                await SeedIfEmptyAsync<ProductsType>(SeedPath, "Types.json", ct);
                await SeedIfEmptyAsync<Product>(SeedPath, "products.json", ct);

                await dbContext.SaveChangesAsync(ct);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to seed data");
                throw;
            }
        }
        private async Task SeedIfEmptyAsync<T>(string root, string fileName, CancellationToken ct) where T : class
        {
            if(await dbContext.Set<T>().AnyAsync(ct)) return;

            var FilePath = Path.Combine(root, fileName);

            if(!File.Exists(FilePath))
            {
                logger.LogWarning($"Seed File Not Found{FilePath}");
                return;
            }
            await using var stream = File.OpenRead(FilePath);

            var items = await JsonSerializer.DeserializeAsync<List<T>>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true} , ct);

            if(items?.Count > 0)
            {
                await dbContext.Set<T>().AddRangeAsync(items, ct);
            }
            await dbContext.SaveChangesAsync(ct);
        }
    }
}
