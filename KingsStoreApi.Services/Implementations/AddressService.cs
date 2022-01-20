using KingsStoreApi.Data.Interfaces;
using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.AddressServiceDTO;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Interfaces;
using System;

namespace KingsStoreApi.Services.Implementations
{
    public class AddressService : IAddressService
    {
        private readonly IRepository<Address> _addressRepoitory;

        public AddressService(IRepository<Address> addressRepoitory)
        {
            addressRepoitory = addressRepoitory ?? throw new ArgumentNullException(nameof(addressRepoitory));
        }
        public ReturnModel AddAddress(AddAddressDTO model)
        {

           
        }

        public ReturnModel RemoveAddress(RemoveAddressDTO model)
        {
            throw new System.NotImplementedException();
        }
    }
}
