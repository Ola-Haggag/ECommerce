using ECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts
{
    public interface IGenericRepository<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task<TEntity?> GetByIdAsync(Tkey id, CancellationToken ct=default);
        Task<TEntity?> GetByIdWithSpecificationsAsync(ISpecifications<TEntity, Tkey> specifications, CancellationToken ct=default);

        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct=default);
        Task<IReadOnlyList<TEntity>> GetAllWithSpecificationsAsync(ISpecifications<TEntity, Tkey> specifications, CancellationToken ct = default);
        
        
        Task<int> GetProductCountWithSpecificationsAsync(ISpecifications<TEntity, Tkey> specifications, CancellationToken ct = default);
    }
}
