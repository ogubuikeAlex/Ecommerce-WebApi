using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.OrderServicesDTO;
using KingsStoreApi.Model.Entities;
using System.Threading.Tasks;

namespace KingsStoreApi.Services.Interfaces
{
    public interface IOrderService
    {
        ReturnModel GetOrderItems(string orderId);
        Task<ReturnModel> AddOrderItem(AddOrderItemDTO model);
        Task<ReturnModel> RemoveOrderItem(string OrderItemId, string orderId);
        Task<ReturnModel> ClearOrder(string orderId);
        ReturnModel GetTotalOrderPrice(string orderId);
    }
}
