using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.CategoryServicesDTO;

namespace KingsStoreApi.Services.Interfaces
{
    public interface ICategoryService
    {
        ReturnModel GetCategory(string categoryName);
        ReturnModel GetAllCategories();
        ReturnModel CreateCategory(CreateCategoryDTO model);
        ReturnModel SoftDeleteCategory(string id);
        ReturnModel UpdateCategory(UpdateCategoryDTO model);
    }
}
