using System;

namespace KingsStoreApi.Model.Entities
{
    public class Product : Entity
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public decimal Price { get; set; }
      
        public int Quantity { get; set; }
        public DateTime PublishedAt { get; set; }
        public byte[] ProductImage { get; set; }

        //Store date of isdeleted
        //if after 30 dyas product is still isdeleted
        //turn on 
    }
}
