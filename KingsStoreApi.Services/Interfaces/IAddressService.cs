using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.AddressServiceDTO;
using KingsStoreApi.Model.Entities;
using System.Threading.Tasks;

namespace KingsStoreApi.Services.Interfaces
{
    public interface IAddressService
    {
        Task<ReturnModel> AddAddress(AddAddressDTO model, User user);
        Task<ReturnModel> RemoveAddress(string id);
    }
}
