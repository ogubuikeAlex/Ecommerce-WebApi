using KingsStoreApi.Model.Entities;
using System.Linq;

namespace KingsStoreApi.Helpers.Implementations.Extensions
{
    public static class ProductExtensions 
    {
        public static IQueryable<Product> Filter (this IQueryable<Product> products, uint maxPrice, uint minPrice)
        {
            return products.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
        }
        
    }
}
