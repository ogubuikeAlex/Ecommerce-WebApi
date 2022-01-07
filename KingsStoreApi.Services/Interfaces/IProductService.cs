using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.ProductServiceDTO;
using KingsStoreApi.Model.DataTransferObjects.SharedDTO;
using KingsStoreApi.Model.Entities;
using System.Threading.Tasks;

namespace KingsStoreApi.Services.Interfaces
{
    public interface IProductService
    {
        ReturnModel GetProductByName(string name);
        ReturnModel GetProductById(string id);
        ReturnModel GetAllProducts();
        Task<ReturnModel> GetDisabledProductsByVendor(string email);
        Task<ReturnModel> GetProductsByVendor(string email);
        Task<ReturnModel> BuyNow();
        ReturnModel UploadProduct(UploadProductDTO model);
        Task<ReturnModel> UplaodProductImage(UploadImageDTO model, User user);//patch
        Task<ReturnModel> EditProductPrice(EditProductDTO model, User user);//patch
        Task<ReturnModel> EditProductSummary(EditProductSummaryDTO model);//patch
        Task<ReturnModel> EditProductTitle(EditProductDTO model, User user);//patch
        Task<ReturnModel> TemporarilyDisableAProduct(string id);
    }
}
