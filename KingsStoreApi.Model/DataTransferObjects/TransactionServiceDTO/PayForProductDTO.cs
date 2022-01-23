namespace KingsStoreApi.Model.DataTransferObjects.TransactionServiceDTO
{
    public class PayForProductDTO
    {
        public decimal amount { get; set; }
        public string orderId { get; set; }
        public string userId { get; set; }
    }
}
