using KingsStoreApi.Model.Entities;

namespace KingsStoreApi.Services.Interfaces
{
    public interface ICartService
    {
        public void AddCartItem(Product product, int quantity);
        public void RemoveCartItem(string cartItemId);
        public void ClearCart();
        public decimal GetTotalCartPrice();
        
    }
}

