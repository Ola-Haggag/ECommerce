using AutoMapper;
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO_s.Baskets;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class BasketServices : IBasketServices
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public BasketServices(IBasketRepository basketRepository, IMapper mapper) 
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
        }
        public async Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, CancellationToken ct = default)
        {
            var CustomerBasket = mapper.Map<CustomerBasket>(basket);
            var Result = await basketRepository.CreateOrUpdateBasketAsync(CustomerBasket, TimeSpan.FromDays(1), ct);

            return Result is not null ? Result<BasketDto>.ok(mapper.Map<BasketDto>(Result)) : Result<BasketDto>.Fail(Error.Failure("CreateOrUpdateBasket.Failure", "Can not Set this Basket"));
        }

        public async Task<Result<bool>> DeleteBasketAsync(string id, CancellationToken ct = default)
        {
            var result = await basketRepository.DeleteBasketAsync(id, ct);

            return result ? Result<bool>.ok(true) : Result<bool>.Fail(Error.Failure("DeleteBasket.Failure", "Can not delete this basket"));
        }

        public async Task<Result<BasketDto>> GetBasketAsync(string id, CancellationToken ct = default)
        {
            var basket = await basketRepository.GetBasketAsync(id, ct);

            if(basket == null)
            {
                return Result<BasketDto>.Fail(Error.NotFound("GetBasket.NotFound", "Can not find this basket"));
            }
            return Result<BasketDto>.ok(mapper.Map<BasketDto>(basket));
        }
    }
}
