using KingsStoreApi.Model.ModelHelpers;
using System;

namespace KingsStoreApi.Model.Entities
{
    public class Category : Entity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
    }
}
