using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.ProductServiceDTO;
using KingsStoreApi.Model.DataTransferObjects.SharedDTO;

namespace KingsStoreApi.Services.Interfaces
{
    public interface IProductService
    {
        ReturnModel GetProductByName(string name);
        ReturnModel GetProductById(string id);
        ReturnModel GetAllProducts();
        ReturnModel UploadProduct(UploadProductDTO model);
        ReturnModel RemoveProduct(RemoveProductDTO model);
        ReturnModel UplaodProductImage(UploadImageDTO model);//patch
        ReturnModel EditProductPrice(EditProductPriceDTO model);//patch
        ReturnModel EditProductSummary(EditProductSummaryDTO model);//patch
        ReturnModel EditProductTitle(EditProductTitleDTO model);//patch
        ReturnModel SoftDeleteProduct(DeleteProductDTO model);        
    }
}
