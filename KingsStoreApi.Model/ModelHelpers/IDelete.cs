using System;

namespace KingsStoreApi.Model.ModelHelpers
{
    public interface IDelete
    {
        bool IsDeleted { get; set; }
        public bool HasBeenRemovedOver30Days { get; }

         
    }
}
