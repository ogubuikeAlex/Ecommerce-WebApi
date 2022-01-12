using System;

namespace KingsStoreApi.Model.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public string CartId { get; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
