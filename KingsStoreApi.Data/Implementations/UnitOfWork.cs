using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KingsStoreApi.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KingsStoreApi.Data.Implementations
{
    public class UnitOfWork<TContext> : IUnitOfWork<DbContext> where TContext : DbContext
    {
        private readonly DbContext _context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(DbContext context)
        {
           _context = context;
        }
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            _repositories ??= _repositories = new Dictionary<Type, object>();

            var entityType = typeof(TEntity);

            if (!_repositories.ContainsKey(entityType))
                _repositories[entityType] = new Repository<TEntity>(_context);

            return (IRepository<TEntity>)_repositories[entityType];
         
        }      
    }
}
