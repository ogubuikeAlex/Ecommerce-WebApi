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

        private lineItemType[] CreateLineItem (Order order)
        {
            var lineItems = new lineItemType[order.OrderItems.Count];
            int count = 0;
            foreach (var item in order.OrderItems)
            {
                //line items to process
                lineItems[count] = new lineItemType
                {
                    itemId = (item.ProductID).ToString(),
                    name = item.ProductName,
                    quantity = item.Quantity,
                    unitPrice = item.UnitPrice
                };
                count++;
            }

            return lineItems;
        }
    }
}
