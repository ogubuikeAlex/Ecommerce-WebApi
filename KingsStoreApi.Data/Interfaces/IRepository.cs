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
        T GetSingleByCondition(Expression<Func<T, bool>> predicate, Func<IQueryable, IOrderedQueryable> orderBy, params string[] includeProperties);
        IEnumerable<T> GetAllByCondition(Expression<Func<T, bool>> predicate, Func<IQueryable, IOrderedQueryable> orderBy, params string[] includeProperties);
    }
}
