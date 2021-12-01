using KingsStoreApi.Helpers.Implementations;

namespace KingsStoreApi.Services.Interfaces
{
    public interface IProductService
    {
        ReturnModel GetProductByName(string name);
        ReturnModel GetProductById(string id);
        ReturnModel GetAllProducts();
        ReturnModel UploadProduct(UploadProductDTO model);
        ReturnModel RemoveProduct(RemoveProductDTO model);
        ReturnModel EditProduct(EditProductDTO model);
        ReturnModel SoftDeleteProduct(DeleteProductDTO model);        
    }
}
