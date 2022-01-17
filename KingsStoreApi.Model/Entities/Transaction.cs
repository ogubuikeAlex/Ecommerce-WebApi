using System;
using KingsStoreApi.Model.Enums;

namespace KingsStoreApi.Model.Entities
{
    public class Transaction : Entity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }       
        public Guid OrderId { get; set; }
        public TransactionType TransactionType { get; set; }
        public TransactionMode TransactionMode { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
    }
}
