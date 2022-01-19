using System;

namespace KingsStoreApi.Model.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Country { get; set; }
        public string HouseNumber { get; set; }
        public string street { get; set; }
        public string City { get; set; }

    }
}
