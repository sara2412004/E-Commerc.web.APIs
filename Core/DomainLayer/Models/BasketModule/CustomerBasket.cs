using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.BasketModule
{
    //msh m7taga a5zno fel DB asln w msh h7wolo l Table wala hyt3ml m3 el Repositry
    public class CustomerBasket
    {
        public string Id { get; set; }//GUID [by frontend]
        public ICollection<BasketItem> Items { get; set; } = [];
        
    }
}
