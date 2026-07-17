using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> InputQuery,ISpecifications<TEntity,
            Tkey> specifications) where TEntity : BaseEntity<Tkey>
        {
            var query = InputQuery;

            if(specifications.IncludeExpression.Count > 0)
            {
                query = specifications.IncludeExpression.Aggregate(query, (current, Expression ) => current.Include(Expression));
            }

            if(specifications.Criteria is not null)
                query = query.Where(specifications.Criteria);

            if (specifications.IsPaginated)
            {
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            }

            if(specifications.OrderBy is not null)
            {
                query = query.OrderBy(specifications.OrderBy);
            }
            if (specifications.OrderByDesc is not null)
            {
                query = query.OrderByDescending(specifications.OrderByDesc);
            }
            return query;
        }
    }
}
