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

            if (await _addressRepoitory.AnyAsync(
                a => a.street == model.street
                && a.UserId == user.Id 
                && a.HouseNumber == model.HouseNumber 
                && a.City == model.City 
                && a.Country == model.Country)
                )
                return new ReturnModel { Message = "Address already exists", Success = false };
            address.UserId = user.Id;
            await _addressRepoitory.AddAsync(address);

            return new ReturnModel { Message = "Address Added Successfully", Success = true };
        }

        public ReturnModel RemoveAddress(string id)
        {
            var address = _addressRepoitory.GetSingleByCondition(a => a.Id.ToString() == id);

            if (address is null)
                return new ReturnModel { Message = "Address not found" };
            await _addressRepoitory.ToggleSoftDeleteAsync(address);
        }
    }
}
