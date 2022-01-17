using KingsStoreApi.Model.Entities;

namespace KingsStoreApi.Model.DataTransferObjects.CartServiceDTO
{
    public class CartItemRepresentationalDTO 
    {         
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}