using KingsStoreApi.Model.Entities;

namespace KingsStoreApi.Model.DataTransferObjects.TransactionServiceDTO
{
    public class CheckoutViewModel
    {
        public Cart Cart { get; set; }
        public decimal DiscountPercent { get; set; }
        public string DiscountName { get; set; }        
        public string Shipping { get; set; }
        public int TotalItemQty { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountAmt { get; set; }
        public decimal Total { get; set; }        
    }
}
