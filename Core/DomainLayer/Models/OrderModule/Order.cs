using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModule
{
    public class Order: BaseEntity<Guid>
    {
        public Order() 
        {

        }
        public Order(string userEmail, OrderAddress address, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subtotal)
        {
            buyerEmail = userEmail;
            shipToAddress = address;
            DeliveryMethod = deliveryMethod;
            Items = items;
            Subtotal = subtotal;
        }

        public string buyerEmail { get; set; } = default!;
        public OrderAddress shipToAddress { get; set; } = default!;
        public decimal Subtotal { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } 
        public int DeliveryMethodId { get; set; }// FK
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public ICollection<OrderItem> Items { get; set; } = [];
        //msh m7taga a5zno fel DB da fel runtime bs [Drived attribuite]
        //[NotMapped]
        //public decimal Total {get=> Subtotal + DeliveryMethod.Price;}
        //....................OR....................
        public decimal GetTotal() => Subtotal + DeliveryMethod.Price;
    }
}
