using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public abstract class BaseSpecifications<TEntity,TKey> : ISpecifications<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        //dol kda haga abstract ll abstract w hro7 ll productwith...
        #region Criteria(Where)
        protected BaseSpecifications(Expression<Func<TEntity, bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }
        public Expression<Func<TEntity, bool>> Criteria { get; private set; } 
        #endregion


        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];
        protected void AddIncludeExpression(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);  // add because its a list 
        }
        #endregion


        #region Sorting
        //Ascending s
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        protected void AddOrderBy(Expression<Func<TEntity, object>> OrderByExp) => OrderBy = OrderByExp; 
        //Descending
        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> OrderByDescendingExp) => OrderByDescending = OrderByDescendingExp;

        #endregion

        #region Pagination
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPaginated { get; set; }
        protected void ApplyPagination(int PageSize,int PageIndex)
        {
            IsPaginated = true;
            Take=PageSize; 
            Skip=(PageIndex-1)*PageSize;
        }
        #endregion



    }
}
