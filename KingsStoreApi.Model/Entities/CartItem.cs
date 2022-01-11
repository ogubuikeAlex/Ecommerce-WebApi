using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingsStoreApi.Model.Entities
{
    public class CartItem
    {
       public Guid Id { get; set; }

        public string CartId { get; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
