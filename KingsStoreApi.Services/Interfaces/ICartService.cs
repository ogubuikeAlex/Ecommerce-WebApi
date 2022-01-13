using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.CartServiceDTO;
using KingsStoreApi.Model.Entities;

namespace KingsStoreApi.Services.Interfaces
{
    public interface ICartService
    {
        ReturnModel AddCartItem(AddToCartDTO model);
        void RemoveCartItem(string cartItemId);
        void ClearCart();
        ReturnModel  GetTotalCartPrice();        
    }
}

