using System.Threading.Tasks;
using KingsStoreApi.Data.Implementations;

namespace KingsStoreApi.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;       
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork { }
}
