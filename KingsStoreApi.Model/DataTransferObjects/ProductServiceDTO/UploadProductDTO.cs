using System;

namespace KingsStoreApi.Model.DataTransferObjects.ProductServiceDTO
{
    public class UploadProductDTO
    {     
        
        public string Title { get; set; }
        public string Summary { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
