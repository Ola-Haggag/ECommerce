using ECommerce.Application.Common;
using ECommerce.Application.DTO_s.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Contracts
{
    public interface IBasketServices
    {
        Task<Result<BasketDto>> GetBasketAsync(string id, CancellationToken ct = default);
        Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, CancellationToken ct = default);
        //Task<bool> DeleteBasketAsync(string id, CancellationToken ct = default);

        Task<Result<bool>> DeleteBasketAsync(string id, CancellationToken ct = default);
    }


}
