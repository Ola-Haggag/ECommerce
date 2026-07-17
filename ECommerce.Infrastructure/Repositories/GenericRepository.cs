using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreDbContext dbContext;

        public GenericRepository(StoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(TEntity entity)
        {
           dbContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }
        public void Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
        }
        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
        {
            return await dbContext.Set<TEntity>().AsNoTracking().ToListAsync(ct);
        }

        public async Task<TEntity?> GetByIdAsync(Tkey id, CancellationToken ct = default)
        {
          return await dbContext.Set<TEntity>().FindAsync(id, ct);  
        }

        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecificationsAsync(ISpecifications<TEntity, Tkey> specifications, CancellationToken ct = default)
        {
            var Result = SpecificationEvaluator.CreateQuery<TEntity, Tkey>(dbContext.Set<TEntity>(), specifications);
            return await Result.ToListAsync(ct);

        }

        public async Task<TEntity?> GetByIdWithSpecificationsAsync(ISpecifications<TEntity, Tkey> specifications, CancellationToken ct = default)
        {
            var Result = SpecificationEvaluator.CreateQuery<TEntity, Tkey>(dbContext.Set<TEntity>(), specifications);
            return await Result.FirstOrDefaultAsync(ct);
        }

        public async Task<int> GetProductCountWithSpecificationsAsync(ISpecifications<TEntity, Tkey> specifications, CancellationToken ct = default)
        {
            var Result = SpecificationEvaluator.CreateQuery<TEntity, Tkey>(dbContext.Set<TEntity>(), specifications);
            return await Result.CountAsync(ct);
        }
    }
}
