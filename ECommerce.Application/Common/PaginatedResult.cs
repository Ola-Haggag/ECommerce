using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Common
{
    public class PaginatedResult<TEntity>
    {
        public PaginatedResult(IReadOnlyList<TEntity> data, int pageIndex, int pageSize , int count)
        {
            this.data = data;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.Count = count;

        }
        public IReadOnlyList<TEntity> data { get; set; } = [];

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
    }
}
