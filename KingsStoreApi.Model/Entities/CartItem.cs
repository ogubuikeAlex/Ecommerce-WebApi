using System;

namespace KingsStoreApi.Model.Entities
{
    public class CartItem : Entity
    {
        public Guid Id { get; set; }
        public string CartId { get; set; }
        public string productId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
