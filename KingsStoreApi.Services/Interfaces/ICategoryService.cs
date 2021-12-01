using KingsStoreApi.Helpers.Implementations;

namespace KingsStoreApi.Services.Interfaces
{
    public interface ICategoryService
    {
        ReturnModel GetCategory(string categoryName);
        ReturnModel GetAllCategories();
        ReturnModel CreateCategory(CreateCategoryDTO model);
        ReturnModel SoftDeleteCategory(SoftDeleteCategoryDTO model);
        ReturnModel UpdateCategory(UpdateCategoryDTO model);
    }
}
