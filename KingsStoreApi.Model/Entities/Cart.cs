using System;
using System.Collections.Generic;
using KingsStoreApi.Model.Enums;

namespace KingsStoreApi.Model.Entities
{
    public class Cart
    {
        private Dictionary<string, Product> _CartContent;
        public Product this[string key]
        {
            get
            {
                if (_CartContent.ContainsKey(key))
                    return _CartContent[key];

                return null;
            }
            set
            {
                if (!_CartContent.ContainsKey(key))
                    _CartContent[key] = value;
            }
        }

            
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string SessionId { get; set; }
        public CartStatus CartStatus { get; set; }
    }
}
