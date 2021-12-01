using System;

namespace KingsStoreApi.Model.Entities
{
    public class Product : Entity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int MyProperty { get; set; }
        public DateTime PublishedAt { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
