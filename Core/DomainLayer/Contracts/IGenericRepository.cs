using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity:BaseEntity<TKey>
    {
        public  Task<IEnumerable<TEntity>> GetAllAsync();
        public Task<TEntity?> GetByIdAsync(TKey id);
        public Task AddAsync(TEntity entity);
        public void Remove(TEntity entity);
        public void Update(TEntity entity);


    }
}
