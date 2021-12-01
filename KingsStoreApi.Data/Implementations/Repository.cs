using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using KingsStoreApi.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KingsStoreApi.Data.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangAsync(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate is null)
                return await _dbSet.AnyAsync();

            return await _dbSet.AnyAsync(predicate);
        }

        public IEnumerable<T> GetAllByCondition(Expression<Func<T, bool>> predicate = null, Func<IQueryable, IOrderedQueryable> orderBy = null, params string[] includeProperties)
        {
            if (predicate is null)
                return _dbSet.ToList();

            var model = _dbSet.Where(predicate);

            foreach (var property in includeProperties)
            {
                foreach (var entity in model)
                {
                    _context.Entry(entity).Reference(property).Load();
                }
            }
            return model;
        }


        public T GetSingleByCondition(Expression<Func<T, bool>> predicate, Func<IQueryable, IOrderedQueryable> orderBy, params string[] includeProperties)
        {
            if (predicate is null)
                return _dbSet.ToList().FirstOrDefault();

            var model = _dbSet.Where(predicate).FirstOrDefault();

            foreach (var property in includeProperties)
            {
                _context.Entry(model).Reference(property).Load();
            }
            return model;
        }
        
    }
}
