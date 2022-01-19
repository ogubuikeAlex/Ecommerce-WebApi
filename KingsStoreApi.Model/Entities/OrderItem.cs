using System;
using System.Collections.Generic;

namespace KingsStoreApi.Model.Entities
{
    public class OrderItem : Entity
    {
        public Guid ID { get; set; }
        public string OrderID { get; set; }
        public string ProductID { get; set; }
        public Product Product { get; set; }        
        public int Quantity { get; set; }    
        
    }
}
