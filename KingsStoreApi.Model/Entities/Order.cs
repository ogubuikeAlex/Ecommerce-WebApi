using System;
using KingsStoreApi.Model.Enums;

namespace KingsStoreApi.Model.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CartId {get; set;}
        public decimal TotalPrice { get; set; }
        public OrderStatus OrderStatusStatus { get; set; }
    }
}
