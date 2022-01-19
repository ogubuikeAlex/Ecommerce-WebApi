using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.Entities;
using System.Threading.Tasks;

namespace KingsStoreApi.Services.Interfaces
{
    public interface IOrderService
    {
        ReturnModel GetOrderItems(string userId);
        Task<ReturnModel> AddOrderItem(User user, string productId, int quantity);
        Task<ReturnModel> RemoveOrderItem(string OrderItemId);
        Task<ReturnModel> ClearOrder(string userId);
        ReturnModel GetTotalOrderPrice(string userId);
    }
}
