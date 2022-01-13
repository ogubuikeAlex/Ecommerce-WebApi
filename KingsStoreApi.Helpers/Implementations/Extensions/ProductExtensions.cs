using KingsStoreApi.Model.Entities;
using System;
using System.Linq;

namespace KingsStoreApi.Helpers.Implementations.Extensions
{
    public static class ProductExtensionst
    {
        public static IQueryable<Product> Filter (this IQueryable<Product> products, uint maxPrice, uint minPrice)
        {
            return products.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
        }
        public static IQueryable<Product> Search(this IQueryable<Product> products, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return products;

            return products.Where(p => p.Title.ToLower().Contains(searchTerm.Trim().ToLower()));
        }
    }
}
