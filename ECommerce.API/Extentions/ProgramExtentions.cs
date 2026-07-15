using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Seeding;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Extentions
{
    public static class ProgramExtentions
    {
        public static async Task MigrationAndSeedAsync(this WebApplication app)
        {
            var Scope =  app.Services.CreateAsyncScope();

            //var DbContext = Scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            //var CatalogLogger = Scope.ServiceProvider.GetRequiredService<ILogger<CatalogDataSeeder>>();

            //var Pending = await DbContext.Database.GetPendingMigrationsAsync();

            //if(Pending.Count() > 0)
            //    await DbContext.Database.MigrateAsync();

            //CatalogDataSeeder catalogDataSeeder = new CatalogDataSeeder(DbContext, CatalogLogger);

            var seeder = Scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
            await seeder.SeedAsync();
        }
    }
}
