using KingsStoreApi.Model.Enums;

namespace KingsStoreApi.Model.DataTransferObjects.CartServiceDTO
{
    public class CartRepresentationalDTO
    {
        public string UserId { get; set; }
        public CartStatus CartStatus { get; set; }
    }
}
