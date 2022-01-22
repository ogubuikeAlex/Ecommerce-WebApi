using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.AddressServiceDTO;
using KingsStoreApi.Model.Entities;

namespace KingsStoreApi.Services.Interfaces
{
    public interface IAddressService
    {
        ReturnModel AddAddress(AddAddressDTO model, User user);
        ReturnModel RemoveAddress(string id);
    }
}
