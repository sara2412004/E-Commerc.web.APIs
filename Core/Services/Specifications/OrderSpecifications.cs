using DomainLayer.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class OrderSpecifications:BaseSpecifications<Order, Guid>
    {
        //get all orders for a specific user
        public OrderSpecifications(string email)
        : base(o => o.buyerEmail == email)
        {
            // navigation property b3mlhom include 3lshan ageb el data bta3thom
            AddIncludeExpression(o => o.Items);
            AddIncludeExpression(o => o.DeliveryMethod);
            AddOrderByDescending(o => o.OrderDate);
        }
        // get order by id
        public OrderSpecifications(Guid id)
            : base(o => o.Id == id )
        {
            AddIncludeExpression(o => o.Items);
            AddIncludeExpression(o => o.DeliveryMethod);
        }
    }
}
