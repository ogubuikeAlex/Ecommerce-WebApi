using KingsStoreApi.Helpers.Implementations;

namespace KingsStoreApi.Services.Interfaces
{
    public interface IOrderService
    {
        ReturnModel CreateOrder(CreateOrderDTO model);
        ReturnModel UpdateOrder(UpdateOrderDTO model);
         
    }
}
