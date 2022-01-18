using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KingsStoreApi.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        public IConfiguration Configuration { get; set; }

        public TransactionService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void PayForProduct(decimal amount, Order order, User user)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            //connection to the API Name and Key for the sandbox
            //define merchant information
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication =
                new merchantAuthenticationType()
                {
                    name = Configuration["AuthorizeNetName"],
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = Configuration["AuthorizeNetItem"],
                };

            var creditCard = CreateCreditCard();
            var billingAddress = CreateBillingAddress(user, order);
        }

        private customerAddressType CreateBillingAddress(User user, Order order)
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

        private lineItemType[] CreateLineItem(Order order)
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

        private creditCardType CreditCardType()
        {
            return new creditCardType
            {
                cardNumber = "4111111111111111",
                expirationDate = "0728",
                cardCode = "123"
            };
        }

        private string ValidateResponse(createTransactionResponse response)
        {
            if (response is null)
                return "invvlid";

            if (response.messages.resultCode != messageTypeEnum.Ok)
                return $"Transaction failed\n{response.transactionResponse.errors[0].errorText ?? response.messages.message[0].code}";

            if (response.transactionResponse.messages is null)
                return "TransactionFailed Error Text: " + response.transactionResponse.errors[0].errorText;
            // We should be getting an OK response type.
            return $"Successfully created transaction with Transaction ID: {response.transactionResponse.transId}\n Response Code: {response.transactionResponse.responseCode}";
        }

        private transactionRequestType CreateTransactionRequestType(decimal amount, paymentType paymentType, customerAddressType billingAddress, lineItemType[] lineItems)
        {
            return new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // charge the card

                amount = amount,
                payment = paymentType,
                billTo = billingAddress,
                lineItems = lineItems
            };
        }

        private creditCardType CreateCreditCard()
        {
            return new creditCardType
            {
                cardNumber = "4111111111111111",
                expirationDate = "0728",
                cardCode = "123"
            };
        }

        private paymentType CreatePaymentType(creditCardType creditCard)
        {
            return new paymentType { Item = creditCard };
        }
    }
}
