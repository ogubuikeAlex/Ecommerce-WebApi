using KingsStoreApi.Model.ModelHelpers;
using System;

namespace KingsStoreApi.Model.Entities
{
    public class Entity : IDelete
    {
        private static DateTime _isDeletedTimeStamp;
        private bool _hasBeenDeletedOver30Days;
        public DateTime? IsDeletedTimeStamp { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool HasBeenRemovedOver30Days
        {
            get => _hasBeenDeletedOver30Days;
            set
            {
                _hasBeenDeletedOver30Days = _isDeletedTimeStamp - DateTime.Now >= TimeSpan.FromDays(30);
            }
        }
    }
}
