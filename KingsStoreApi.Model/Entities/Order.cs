using System.Collections.Generic;

namespace KingsStoreApi.Model.Entities
{
    public class Order
    {
        public int ID { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string UserID { get; set; }
        public string AddressID { get; set; } 
        public Address Address { get; set; }
        public string OrderDate { get; set; }
        public int TotalItemQty { get; set; }
        public string DiscountName { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmt { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
    }
}
