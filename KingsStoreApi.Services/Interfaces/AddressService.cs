using KingsStoreApi.Helpers.Implementations;

namespace KingsStoreApi.Services.Interfaces
{
    public interface AddressService
    {
        ReturnModel AddAddress(AddAddressDTO model);
        ReturnModel RemoveAddress(RemoveAddressDTO model);
    }
}
