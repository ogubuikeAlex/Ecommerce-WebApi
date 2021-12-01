using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.Entities;

namespace KingsStoreApi.Services.Interfaces
{
    public interface ICartService
    {
        ReturnModel GetCart(string id);
        ReturnModel GetAllCarts();
        ReturnModel CreateCart(CreateCartDTO model);
        ReturnModel AddToCart(params Product[] products);
        ReturnModel RemoveFromCart(params Product[] products);
        ReturnModel UpdateCart(UpdateCartDTO model);
    }
}
