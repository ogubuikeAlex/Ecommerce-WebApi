using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using KingsStoreApi.Data.Interfaces;
using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.TransactionServiceDTO;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Model.Enums;
using KingsStoreApi.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingsStoreApi.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<Discount> _discountRepository;

        public IConfiguration Configuration { get; set; }

        public TransactionService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            Configuration = configuration;
            _addressRepository = unitOfWork.GetRepository<Address>();
            _orderRepository = unitOfWork.GetRepository<Order>();
            _orderItemRepository = unitOfWork.GetRepository<OrderItem>();
            _discountRepository = unitOfWork.GetRepository<Discount>();
        }        

        public string PayForProduct(decimal amount, Order order, User user)
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
            var paymentType = CreatePaymentType(creditCard);
            var lineItems = CreateLineItem(order);
            var transactionRequest = CreateTransactionRequestType(amount, paymentType, billingAddress, lineItems);

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            var controller = new createTransactionController(request);
            controller.Execute();

            var response = controller.GetApiResponse();

            var result = ValidateResponse(response);
            //change to a multilevel string check!
            return result;
        }

        public async Task<ReturnModel> ConfirmOrder(ConfirmTransactionDTO confirmTransactionModel, User user)
        {
            //I should be able to get a cart from a user
            Cart cart = user.Cart;

            var discount = _discountRepository.GetSingleByCondition(d => d.Name == confirmTransactionModel.DiscountName);

            if (cart.CartItems.Count == 0)
                return new ReturnModel { Success = false, Message = "Your Cart is empty" };           

            // add address to database if it does not already exist!
           // await _addressRepository.AddAsync(confirmTransactionModel.Address);
            // create an new order object and load the order items onto it
            Order datOrder = new Order
            {
                UserID = user.Id,
                AddressID = confirmTransactionModel.Address.Id.ToString(),
                Address = confirmTransactionModel.Address,
                OrderDate = DateTime.Now.ToString("MMM d, yyyy (ddd) @ HH:mm tt"),
                DiscountName = confirmTransactionModel.DiscountName,                
                TotalItemQty = confirmTransactionModel.TotalItemQty,
                Subtotal = confirmTransactionModel.Subtotal,
                Total = confirmTransactionModel.Total,
            };

            // add order to the database table
            // I'm doing this first in hopes that the order generates an ID that
            // I can add to the order items. Here's hoping...
            await _orderRepository.AddAsync(datOrder);

            //call method to convert all cartitems to orderItems, return orderItems as demOrderItems 
            var demOrderItems = new List<OrderItem>();
            // attach orderitems to order
            datOrder.OrderItems = demOrderItems;

            //sends a receipt of the order information /
            //PRIVATE FUNCTION that takes in a order generates a message
            string htmlMessage = "Thank you for shopping with us!  You ordered: </br>";
            foreach (var item in datOrder.OrderItems)
            {
                htmlMessage += $"Item: {item.Product.Title}, Quantity: {item.Quantity}</br>";
            };

            //CHARGE CARD --> private method
            //Payment payment = new Payment(Configuration);
            PayForProduct(confirmTransactionModel.Total, datOrder, user);

            /*await _emailSender.SendEmailAsync(user.Email, "Order Information",
                        htmlMessage);
            // empty out basket
            await _basketRepo.ClearOutBasket(confirmTransactionModel.Basket.BasketItems);

            return "done";*/
            return new ReturnModel { };
        }

        private decimal ApplyDiscountCode(decimal initialAmount, Discount discount)
            => discount.PercentageOff / 100 * initialAmount;   
       

        //Should convert into a private method that takes a cart, convert the cartItems into orderitems and adds then to thedb 
        private async Task<List<OrderItem>> convertCartItemToOrderItem(List<CartItem> cartItems)
        {
            List<OrderItem> demOrderItems = new List<OrderItem>();
            foreach (var item in cartItems)
            {
                OrderItem tempOrderItem = new OrderItem
                {
                    ProductID = item.ProductId,
                    OrderID = item.CartId,
                    Product = item.Product,
                    Quantity = item.Quantity,
                };

                // add order item to the database table
                await _orderItemRepository.AddAsync(tempOrderItem);
                demOrderItems.Add(tempOrderItem);
            }

            return demOrderItems;
        }

        private customerAddressType CreateBillingAddress(User user, Order order)
        {
            return new customerAddressType
            {
                firstName = user.FullName,
                email = user.Email,
                country = "Nigeria",
                address = order.Address.HouseNumber + order.Address.street,
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
                    name = item.Product.Title,
                    quantity = item.Quantity,
                    unitPrice = item.Product.Price
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
