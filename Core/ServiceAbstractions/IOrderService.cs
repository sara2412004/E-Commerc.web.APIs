using Shared.DataTransferObjects.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        //Create Order
        Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string Email);
        //Get All Delivery Methods
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();
        //Get All Orders By Email
        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email);
        //Get Order By Id
        Task<OrderToReturnDto> GetOrderByIdAsync(Guid id);
    }
}
