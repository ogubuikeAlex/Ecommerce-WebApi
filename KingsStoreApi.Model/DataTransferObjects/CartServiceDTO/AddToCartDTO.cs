using KingsStoreApi.Model.Entities;

namespace KingsStoreApi.Model.DataTransferObjects.CartServiceDTO
{
    public class AddToCartDTO
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
