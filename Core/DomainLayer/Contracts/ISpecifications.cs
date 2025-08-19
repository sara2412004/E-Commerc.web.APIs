using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey> 
    {
        //Property signature for each Dynamic part in Query 
        public Expression<Func<TEntity,bool>> Criteria { get; } //Where 
        public List<Expression<Func<TEntity,object>>> IncludeExpressions { get; } //object like(brandname in productDto)
        #region Sorting
        public Expression<Func<TEntity, object>> OrderBy { get; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; }
        #endregion

        #region Pagination
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginated {  get; set; }

        #endregion
    }
}
