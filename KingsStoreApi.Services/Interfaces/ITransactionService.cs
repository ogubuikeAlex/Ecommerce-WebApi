using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.TransactionServiceDTO;
using KingsStoreApi.Model.Entities;
using System.Threading.Tasks;

namespace KingsStoreApi.Services.Interfaces
{
    public interface ITransactionService
    {
        string PayForProduct(decimal amount, Order order, User user);
        Task<ReturnModel> ConfirmOrder(ConfirmTransactionDTO confirmTransactionModel, User user);
        ReturnModel AddDiscountCode(string code);
    }
}
