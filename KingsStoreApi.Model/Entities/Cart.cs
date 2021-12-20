using System;
using System.Collections.Generic;
using System.Linq;
using KingsStoreApi.Model.Enums;

namespace KingsStoreApi.Model.Entities
{
    public class Cart
    {
        private List<CartItem> _CartContent;

        public Cart()
        {
            _CartContent = new List<CartItem>();
        }
        public void AddCartItem(Product product, int quantity)
        {
            var cartItem = _CartContent.Where(p => p.Product.Id == product.Id).FirstOrDefault();

            if (cartItem is null)
            {
                var newCartItem = new CartItem {Product = product, Quantity = quantity };
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
        
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string SessionId { get; set; }
        public CartStatus CartStatus { get; set; }
         class CartItem
         {
            public CartItem()
            {
                CartId = "785";
            }
            public string CartId { get; }
            public Product Product { get; set; }
            public int Quantity { get; set; }
         }
    }

    
}

