using KingsStoreApi.Data.Interfaces;
using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.CartServiceDTO;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Interfaces;
using System;

namespace KingsStoreApi.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork unitOfWork;
        private IRepository<Cart> _repository;
        private IRepository<CartItem> _cartItemRepository;

        public CartService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Cart>();
            _cartItemRepository = unitOfWork.GetRepository<CartItem>();
        }
        /*private List<CartItem> _CartContent;
        
        public CartService()
        {
            _CartContent = new List<CartItem>();
        }*/
        public void CreateCart()
        {

        }
        public ReturnModel AddCartItem(AddToCartDTO model)
        {
            var cartItem = _cartItemRepository.GetSingleByCondition(p => p.Product.Id == model.Product.Id && p.CartId == model.Cart.Id.ToString());

            if (cartItem is null)
            {
                var newCartItem = new CartItem
                {
                    Product = model.Product,
                    Quantity = model.Quantity,
                    CartId = model.Cart.Id.ToString(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _cartItemRepository.AddAsync(newCartItem);
                model.Cart?.CartContent?.Add(cartItem);
                _repository.Update();

                return new ReturnModel {Success = true, Message= "Cart Item Added", Object = cartItem };
            }

            cartItem.Quantity++;
            _cartItemRepository.Update();
            return new ReturnModel {Success = false, Message = "Cart item Quantity increased" };
        }

        public void RemoveCartItem(string cartItemId)
        {
            /* var cartItem = _CartContent.Where(c => c.CartId == cartItemId).FirstOrDefault();

             if (cartItem is not null)
                 _CartContent.Remove(cartItem);*/
        }

        public void ClearCart()
        {
            /*_CartContent.Clear();*/
        }
        public decimal GetTotalCartPrice()
        {
            decimal totalPrice = 0;

            /* foreach (var item in _CartContent)
            {
                item.Product.Price += totalPrice;
            }*/

            return totalPrice;
        }

    }
}