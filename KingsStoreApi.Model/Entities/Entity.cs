using KingsStoreApi.Model.ModelHelpers;
using System;

namespace KingsStoreApi.Model.Entities
{
    public class Entity : ISoftDelete
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get ; set ; }

    }
}
