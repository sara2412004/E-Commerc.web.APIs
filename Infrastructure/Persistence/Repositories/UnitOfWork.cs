using DomainLayer.Contracts;
using DomainLayer.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories =[];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            //1.Get type name 
            var typeName= typeof(TEntity).Name;
            //2. create dictionary to store repositories 
            // check is the repo in dic or not 
            if(_repositories.TryGetValue(typeName,out object value))
                return (IGenericRepository<TEntity, TKey>)value; //explicite casting
            //............... if not in dic........................ ????
            //1. create object 
            var Repo = new GenericRepository<TEntity, TKey>(_dbContext);
            //2. save object in dic 
            _repositories[typeName] = Repo;
            //3. return object 
            return Repo;
            
        }

        public async Task<int> SaveChangesAsync()
        {
           return await  _dbContext.SaveChangesAsync();
        }
    }
}
