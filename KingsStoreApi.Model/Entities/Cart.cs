using KingsStoreApi.Model.Enums;
using System;
using System.Collections.Generic;

namespace KingsStoreApi.Model.Entities
{
    public class Cart : Entity
    {
        public Cart()
        {
            CartContent = new List<CartItem>();
        }
        public  List<CartItem> CartContent;
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public CartStatus CartStatus { get; set; }        
    }
}
