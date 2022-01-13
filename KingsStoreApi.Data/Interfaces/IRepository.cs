using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KingsStoreApi.Data.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> AddAsync(T t);
        Task<IEnumerable<T>> AddRangAsync(IEnumerable<T> t);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        T GetSingleByCondition(Expression<Func<T, bool>> predicate = null, Func<IQueryable , IOrderedQueryable> orderBy = null, params string[] includeProperties);
        IQueryable<T> GetAllByCondition(Expression<Func<T, bool>> predicate = null, Func<IQueryable, IOrderedQueryable> orderBy = null, params string[] includeProperties);
        Task<bool> ToggleSoftDeleteAsync(T t);
        Task UpdateAsync();
    }
}
