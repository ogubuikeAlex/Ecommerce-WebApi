using System;
using System.Collections.Generic;

namespace KingsStoreApi.Model.Entities
{
    public class OrderItem
    {
        public Guid ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }        
        public int Quantity { get; set; }    
        
    }
}
