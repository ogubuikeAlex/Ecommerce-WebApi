using KingsStoreApi.Data.Interfaces;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Model.Enums;
using KingsStoreApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KingsStoreApi.Services.Implementations
{
    public class CartService : ICartService 
    {
        private readonly IUnitOfWork unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /*private List<CartItem> _CartContent;
        
        public CartService()
        {
            _CartContent = new List<CartItem>();
        }*/
        public void CreateCart()
        {

        }
        public void AddCartItem(Product product, int quantity)
        {
            var cartItem = _CartContent.Where(p => p.Product.Id == product.Id).FirstOrDefault();

            if (cartItem is null)
            {
                var newCartItem = new CartItem { Product = product, Quantity = quantity };
                _CartContent.Add(newCartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
        }

        public void RemoveCartItem(string cartItemId)
        {
            var cartItem = _CartContent.Where(c => c.CartId == cartItemId).FirstOrDefault();

            if (cartItem is not null)
                _CartContent.Remove(cartItem);
        }

        public void ClearCart()
        {
            _CartContent.Clear();
        }
        public decimal GetTotalCartPrice()
        {
            decimal totalPrice = 0;

            foreach (var item in _CartContent)
            {
                item.Product.Price += totalPrice;
            }

            return totalPrice;
        }       
        
    }
}