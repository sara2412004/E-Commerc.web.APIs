using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description{ get; set; }=null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public ProductBrand ProductBrand { get; set; } = null!;
        public int BrandId { get; set; } //FK
        public ProductType ProductType { get; set; } = null!;
        public int TypeId { get; set; } //Fk

    }
}
