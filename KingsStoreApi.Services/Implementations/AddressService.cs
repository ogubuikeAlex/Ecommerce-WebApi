using AutoMapper;
using KingsStoreApi.Data.Interfaces;
using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.AddressServiceDTO;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace KingsStoreApi.Services.Implementations
{
    public class AddressService : IAddressService
    {
        private readonly IRepository<Address> _addressRepoitory;
        public readonly IMapper _mapper;

        public AddressService(IRepository<Address> addressRepoitory, IMapper mapper)
        {
           _addressRepoitory = addressRepoitory ?? throw new ArgumentNullException(nameof(addressRepoitory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ReturnModel> AddAddress(AddAddressDTO model, User user)
        {
            var address = _mapper.Map<Address>(model);

            address.UserId = user.Id;
            await _addressRepoitory.AddAsync(address);
           
        }

        public ReturnModel RemoveAddress(RemoveAddressDTO model)
        {
            throw new System.NotImplementedException();
        }
    }
}
