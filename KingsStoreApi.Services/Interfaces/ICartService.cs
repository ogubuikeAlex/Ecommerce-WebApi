using KingsStoreApi.Model.Entities;
using KingsStoreApi.Model.Enums;
using System;

namespace KingsStoreApi.Services.Interfaces
{
    public interface ICartService
    {
        public void AddCartItem(Product product, int quantity);
        public void RemoveCartItem(string cartItemId);
        public void ClearCart();
        public decimal GetTotalCartPrice();
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string SessionId { get; set; }
        public CartStatus CartStatus { get; set; }
    }
}

