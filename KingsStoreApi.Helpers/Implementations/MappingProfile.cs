using AutoMapper;
using KingsStoreApi.Model.DataTransferObjects.CategoryServicesDTO;
using KingsStoreApi.Model.DataTransferObjects.UserServiceDTO;
using KingsStoreApi.Model.Entities;

namespace KingsStoreApi.Helpers.Implementations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateMap<From, To>()
            CreateMap<RegisterDTO, User>();
            CreateMap<CreateCategoryDTO, Category>();
        }
    }
}
