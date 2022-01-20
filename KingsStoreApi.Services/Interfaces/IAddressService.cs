using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.AddressServiceDTO;

namespace KingsStoreApi.Services.Interfaces
{
    public interface IAddressService
    {
        ReturnModel AddAddress(AddAddressDTO model);
        ReturnModel RemoveAddress(RemoveAddressDTO model);
    }
}
