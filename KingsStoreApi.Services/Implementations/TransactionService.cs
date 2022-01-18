using AuthorizeNet.Api.Contracts.V1;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Interfaces;

namespace KingsStoreApi.Services.Implementations
{
    public class TransactionService : ITransactionService
    {

        public void PayForProduct()
        {
            throw new System.NotImplementedException();
        }
        
        private customerAddressType CreateBillingAddress (User user, Order order)
        {
            return new customerAddressType
            {
                firstName = user.FullName,
                email = user.Email,
                country = "Nigeria",               
                address = order.Shipping,
                city = "Enugu",
                zip = "100403"
            };
        }

    }
}
