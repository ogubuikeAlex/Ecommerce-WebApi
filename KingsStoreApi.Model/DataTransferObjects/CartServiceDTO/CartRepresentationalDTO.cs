using KingsStoreApi.Model.Enums;

namespace KingsStoreApi.Model.DataTransferObjects.CartServiceDTO
{
    class CartRepresentationalDTO
    {
        public string UserId { get; set; }
        public CartStatus CartStatus { get; set; }
    }
}
