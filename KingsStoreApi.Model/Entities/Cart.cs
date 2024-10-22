﻿using KingsStoreApi.Model.Enums;
using System;
using System.Collections.Generic;

namespace KingsStoreApi.Model.Entities
{
    public class Cart : Entity
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
        }
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public CartStatus CartStatus { get; set; } 
        public List<CartItem> CartItems { get; set; }
    }
}
