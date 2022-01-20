using KingsStoreApi.Model.Entities;

namespace KingsStoreApi.Model.DataTransferObjects.TransactionServiceDTO
{
    public class ConfirmTransactionDTO
    {
        public Cart Cart { get; set; }        
        public string DiscountName { get; set; }        
        public string Shipping { get; set; }
        public int TotalItemQty { get; set; }
        public decimal Subtotal { get; set; }        
        public decimal Total { get; set; }  
        public Address Address { get; set; }
    }
}
