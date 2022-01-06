using System.Threading.Tasks;
using KingsStoreApi.Data.Implementations;
using KingsStoreApi.Model.ModelHelpers;

namespace KingsStoreApi.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class, IDelete;       
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork { }
}
