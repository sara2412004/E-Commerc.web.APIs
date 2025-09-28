using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModule
{
    // el hagat el thabta l kol order msh far2 fe haga tanzeem bs
    public class ProductItemOrdered
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }=default!;
        public string PictureUrl { get; set; } = default!;
    }
}
