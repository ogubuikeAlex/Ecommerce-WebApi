using KingsStoreApi.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingsStoreApi.Model.Entities
{
    public class Cart : IEntity
    {
        private List<CartItem> _CartContent;
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string SessionId { get; set; }
        public CartStatus CartStatus { get; set; }        
    }
}
