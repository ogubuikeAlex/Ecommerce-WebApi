using KingsStoreApi.Model.Entities;

namespace KingsStoreApi.Model.DataTransferObjects.CartServiceDTO
{
    public class AddToCartDTO
    {
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
