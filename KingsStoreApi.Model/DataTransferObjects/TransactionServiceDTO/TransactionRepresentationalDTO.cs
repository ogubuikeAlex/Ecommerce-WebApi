using KingsStoreApi.Model.Enums;
using System;

namespace KingsStoreApi.Model.DataTransferObjects.TransactionServiceDTO
{
    public class TransactionRepresentationalDTO
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public TransactionType TransactionType { get; set; }
        public TransactionMode TransactionMode { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
    }
}
