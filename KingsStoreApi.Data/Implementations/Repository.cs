using KingsStoreApi.Data.Interfaces;
using KingsStoreApi.Model.ModelHelpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KingsStoreApi.Data.Implementations
{
    public class Repository<T> : IRepository<T> where T : class, IDelete
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        private Expression<Func<T, bool>> _isDeleted = s => !s.IsDeleted;

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
                return await _dbSet.Where(_isDeleted).AnyAsync();

            return await _dbSet.Where(_isDeleted).AnyAsync(predicate);
        }

        public IQueryable<T> GetAllByCondition(Expression<Func<T, bool>> predicate = null, Func<IQueryable, IOrderedQueryable> orderBy = null, bool includeIsDeleted = false, params string[] includeProperties)
        {
            if (predicate is null)
                return includeIsDeleted ? _dbSet : _dbSet.Where(_isDeleted);

            var model = includeIsDeleted ? _dbSet.Where(predicate) : _dbSet.Where(_isDeleted).Where(predicate);

            foreach (var property in includeProperties)
            {
                foreach (var entity in model)
                {
                    _context.Entry(entity).Reference(property).Load();
                }
            }
            return model;
        }

        public T GetSingleByCondition(Expression<Func<T, bool>> predicate, Func<IQueryable, IOrderedQueryable> orderBy, bool includeIsDeleted = false, params string[] includeProperties)
        {
            var model = includeIsDeleted ? _dbSet.Where(predicate).FirstOrDefault() : _dbSet.Where(_isDeleted).Where(predicate).FirstOrDefault();

            foreach (var property in includeProperties)
            {
                _context.Entry(model).Reference(property).Load();
            }

            return model;
        }

        public async Task<bool> ToggleSoftDeleteAsync(T t)
        {
            if (t.IsDeleted) 
            {
                t.IsDeleted = false;
                await _context.SaveChangesAsync();
                return t.IsDeleted;
            }

            t.IsDeleted = true;
            await _context.SaveChangesAsync();
            return t.IsDeleted;
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
