using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Helpers.Implementations.RequestFeatures;
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
        ReturnModel GetAllProducts(ProductRequestParameters requestParameters);
        ReturnModel GetDisabledProductsByVendor(User user, ProductRequestParameters requestParameters);
        ReturnModel GetProductsByVendor(User user, ProductRequestParameters requestParameters);
        Task<ReturnModel> UploadProduct(UploadProductDTO model, User user);
        Task<ReturnModel> UplaodProductImage(UploadImageDTO model, User user);//patch
        Task<ReturnModel> EditProductPrice(EditProductDTO model, User user);//patch
        Task<ReturnModel> EditProductSummary(EditProductDTO model, User user);//patch
        Task<ReturnModel> EditProductTitle(EditProductDTO model, User user);//patch
        Task<ReturnModel> TemporarilyDisableAProduct(string id);
    }
}
