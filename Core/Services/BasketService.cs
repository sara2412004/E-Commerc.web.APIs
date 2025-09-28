using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BasketService(IBasketRepository _Repo,IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> GetBasketAsync(string key)
        {
            var Basket=await _Repo.GetBasketAsync(key);
            if (Basket is not null)
            {
                var BasketDto = _mapper.Map<BasketDto>(Basket);
                return BasketDto;
            }
            else
            {
                throw new BasketNotFoundException(key);
            }
        }
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto)
        {
            var CustomerBasket = _mapper.Map<BasketDto,CustomerBasket>(basketDto);   
            var CreateOrUpdate=await _Repo.CreateOrUpdateBasketAsync(CustomerBasket);
           if(CreateOrUpdate is not null)
            {
                return await GetBasketAsync(basketDto.Id);
            }
            else
            {
                throw new Exception("Failed to create or update basket");
            }
        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
            var result =await _Repo.DeleteBasketAsync(key);
            return result;
        }

    }
}
