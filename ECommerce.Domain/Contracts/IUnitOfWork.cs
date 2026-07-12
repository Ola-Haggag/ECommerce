using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Common;

namespace ECommerce.Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct=default);

        IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>()
             where TEntity : BaseEntity<Tkey>;
    }
}
