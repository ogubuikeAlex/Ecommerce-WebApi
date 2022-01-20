using System;

namespace KingsStoreApi.Model.Entities
{
    public class Discount : Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public byte PercentageOff { get; set; }
        public int NumberOfBenefeciaries { get; set; }
       
    }
}
