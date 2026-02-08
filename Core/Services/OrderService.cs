using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using Service.Specifications;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDtos;
using Shared.DataTransferObjects.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService(IBasketRepository _repository, IMapper _mapper, IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string Email)
        {
            //mapp addressDto to order address
            var OrderAddress = _mapper.Map<AddressDto, OrderAddress>(orderDto.shipToAddress);
            //Get Basket 3lshan ageb mno el items[3lshan ana m3ia el basketid]
            var Basket =await _repository.GetBasketAsync(orderDto.BasktId)??throw new BasketNotFoundException(orderDto.BasktId);
            // create orderitem list
            List<OrderItem> OrderItems = [];
            var ProductRepo=_unitOfWork.GetRepository<Product,int>();
            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                var orderItems = new OrderItem
                {
                    Product = new ProductItemOrdered() 
                    {
                        ProductId= Product.Id,
                        ProductName= Product.Name,
                        PictureUrl= Product.PictureUrl
                    },
                    Price = Product.Price,
                    Quantity = item.Quantity
                };
                OrderItems.Add(orderItems);

            };

            //Get Delivery Method
            var DeliveryMethod =await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId)??throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);

            //caluc subtotal 
            var subtotal = OrderItems.Sum(item => item.Price * item.Quantity);
            //Create Order
            var Order = new Order(Email,OrderAddress, DeliveryMethod,OrderItems,subtotal);// ctor 
           await _unitOfWork.GetRepository<Order,Guid>().AddAsync(Order);  
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<Order,OrderToReturnDto>(Order);
        }
        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDto>>(DeliveryMethods);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email)
        {
            var Spec = new OrderSpecifications(Email);
            var Orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(Spec);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(Orders);
        }
        public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid id)
        {
            var Spec = new OrderSpecifications(id);
            var Order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(Spec) ?? throw new OrderNotFoundException(id);
            return _mapper.Map<Order, OrderToReturnDto>(Order);
        }
        
    }
}
