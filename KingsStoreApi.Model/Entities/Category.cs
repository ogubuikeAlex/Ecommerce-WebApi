using System;

namespace KingsStoreApi.Model.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
    }
}
