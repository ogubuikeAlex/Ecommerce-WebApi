using System.Collections.Generic;

namespace KingsStoreApi.Model.Enums
{
    public class Discounts
    {
        private Dictionary<string, int?> _discounts;

        public Discounts()
        {
            _discounts = new Dictionary<string, int?>
            {
                
                ["OwambeBlockChain"] = 5,
                ["BigBoyBroom"] = 10,
                ["KpomoKatrina"] = 15,
                ["Chiefkpafuka"] = 20,
                ["OgaKpakpata"] = 50,
                ["OshofreeGettat"] = 100
            };

        }
        public int? this[string discountName] => _discounts[discountName] ??= default; 
        
    }
}
