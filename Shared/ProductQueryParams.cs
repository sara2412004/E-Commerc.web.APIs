using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParams
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions sortingOptions {  get; set; }
        public int PageIndex { get; set; } = 1;
        //3lshan law mb3tsh haga 
        private const int DefaultPageSiza = 5;
        private const int MaxPageSiza = 10;

        private int pageSize = DefaultPageSiza;
        public int PageSize
        {
            get
            { return pageSize; }
            set { pageSize = value > MaxPageSiza ? MaxPageSiza : value; }

        }
    }
}
