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
            UserEmail = userEmail;
            Address = address;
            DeliveryMethod = deliveryMethod;
            Items = items;
            Subtotal = subtotal;
        }

        public string UserEmail { get; set; } = default!;
        public OrderAddress Address { get; set; } = default!;// one to one mandatory relation
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public ICollection<OrderItem> Items { get; set; } = [];
        //m3mltsh fel orderitem el relation 3shan msh h7taga eny mn el orderitem ageeb el order ely hwa feh msh hyfedny fe haga
        public decimal Subtotal { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus OrderStatus { get; set; } 
        public int DeliveryMethodId { get; set; }// Foreign Key
        //msh m7taga a5zno fel DB da fel runtime bs [Drived attribuite]
        //[NotMapped]
        //public decimal Total {get=> Subtotal + DeliveryMethod.Price;}
        //....................OR....................
        public decimal GetTotal() => Subtotal + DeliveryMethod.Price;
    }
}
