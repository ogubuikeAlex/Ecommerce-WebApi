using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.CartServiceDTO;
using KingsStoreApi.Model.Entities;

namespace KingsStoreApi.Services.Interfaces
{
    public interface ICartService
    {
        public ReturnModel AddCartItem(AddToCartDTO model);
        public void RemoveCartItem(string cartItemId);
        public void ClearCart();
        public decimal GetTotalCartPrice();        
    }
}

