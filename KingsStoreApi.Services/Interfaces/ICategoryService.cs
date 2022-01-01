using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.CategoryServicesDTO;
using System.Threading.Tasks;

namespace KingsStoreApi.Services.Interfaces
{
    public interface ICategoryService
    {
        ReturnModel GetCategory(string categoryName);
        ReturnModel GetAllCategories();
        Task<ReturnModel> CreateCategory(CreateCategoryDTO model);
        Task<ReturnModel> ToggleSoftDeleteCategory(string id);
        ReturnModel UpdateCategory(UpdateCategoryDTO model);

    }
}
