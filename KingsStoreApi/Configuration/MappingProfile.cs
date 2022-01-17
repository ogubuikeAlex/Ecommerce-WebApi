using AutoMapper;
using KingsStoreApi.Model.DataTransferObjects.CartServiceDTO;
using KingsStoreApi.Model.DataTransferObjects.CategoryServicesDTO;
using KingsStoreApi.Model.DataTransferObjects.ProductServiceDTO;
using KingsStoreApi.Model.DataTransferObjects.TransactionServiceDTO;
using KingsStoreApi.Model.DataTransferObjects.UserServiceDTO;
using KingsStoreApi.Model.Entities;

namespace KingsStoreApi.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateMap<From, To>()
            CreateMap<RegisterDTO, User>();
            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<UploadProductDTO, Product>();
            CreateMap<CartItem, CartItemRepresentationalDTO>();
            CreateMap<Cart, CartRepresentationalDTO>();
            CreateMap<Product, ProductRepresentationalDTO>();
            CreateMap<User, UserRepresentationalDTO>();
            CreateMap<Transaction, TransactionRepresentationalDTO>();
        }
    }
}
