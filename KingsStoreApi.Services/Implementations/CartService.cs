using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Interfaces;

namespace KingsStoreApi.Services.Implementations
{
    public class CartService : ICartService
    {
        public ReturnModel AddToCart(params Product[] products)
        {
            throw new System.NotImplementedException();
        }

        public ReturnModel CreateCart(CreateCartDTO model)
        {
            throw new System.NotImplementedException();
        }

        public ReturnModel GetAllCarts()
        {
            throw new System.NotImplementedException();
        }

        public ReturnModel GetCart(string id)
        {
            throw new System.NotImplementedException();
        }

        public ReturnModel RemoveFromCart(params Product[] products)
        {
            throw new System.NotImplementedException();
        }

        public ReturnModel UpdateCart(UpdateCartDTO model)
        {
            throw new System.NotImplementedException();
        }
    }
}
