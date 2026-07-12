using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public class UnitOfWork(StoreDbContext dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _Repos = [];
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            var typeName = typeof(TEntity).Name;
            if (_Repos.TryGetValue(typeName, out object OldRepos))
                return (IGenericRepository<TEntity, Tkey>) OldRepos;

            var NewRepo = new GenericRepository<TEntity, Tkey>(dbContext);

            _Repos[typeName] = NewRepo;

            return NewRepo;
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
           return await dbContext.SaveChangesAsync(ct);
        }
    }
}
