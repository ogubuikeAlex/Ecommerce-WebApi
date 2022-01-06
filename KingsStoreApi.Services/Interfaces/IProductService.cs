using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.ProductServiceDTO;
using KingsStoreApi.Model.DataTransferObjects.SharedDTO;
using System.Threading.Tasks;

namespace KingsStoreApi.Services.Interfaces
{
    public interface IProductService
    {
        ReturnModel GetProductByName(string name);
        ReturnModel GetProductById(string id);
        ReturnModel GetAllProducts();
        Task<ReturnModel> GetDiabledProductsByVendor(string email);
        Task<ReturnModel> GetProductsByVendor(string email);
        Task<ReturnModel> BuyNow();
        ReturnModel UploadProduct(UploadProductDTO model);
        ReturnModel UplaodProductImage(UploadImageDTO model);//patch
        ReturnModel EditProductPrice(EditProductPriceDTO model);//patch
        ReturnModel EditProductSummary(EditProductSummaryDTO model);//patch
        ReturnModel EditProductTitle(EditProductTitleDTO model);//patch
        Task<ReturnModel> TemporarilyDisableAProduct(string id);
        Task<ReturnModel> PermanentlyDisableAProduct(DeleteProductDTO model);
    }
}
