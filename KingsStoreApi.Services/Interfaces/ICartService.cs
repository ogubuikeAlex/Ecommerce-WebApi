using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.CartServiceDTO;
using KingsStoreApi.Model.Entities;
using System.Threading.Tasks;

namespace KingsStoreApi.Services.Interfaces
{
    public interface ICartService
    {
        ReturnModel GetCartItems(string userId); 
        Task<ReturnModel> AddCartItem(User user, string productId, int quantity);
        Task<ReturnModel> RemoveCartItem(string cartItemId);
        Task<ReturnModel> ClearCart(string userId);
        ReturnModel  GetTotalCartPrice(string userId);        
    }
}

