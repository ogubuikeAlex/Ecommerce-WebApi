using System;
using System.Collections.Generic;
using System.Linq;

namespace KingsStoreApi.Helpers.Implementations.RequestFeatures
{
    /// <summary>
    /// PagedList by inheriting List becomes a list on its own
    /// we can add to it, etc
    /// Metadata: Hold info about thislist
    /// Constrcutor: We got a a list of resources we want to page then we add it to the PagedList
    /// then we use the remaining info to initialize the metadata
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : List<T> 
    {
        public Metadata Metadata { get; set; }
        public PagedList(List<T> items, int itemsTotalCount, int pageNumber, int pageSize)
        {
            Metadata = new Metadata
            {
                TotalCount = itemsTotalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(itemsTotalCount/ (double)pageSize) 
            };
            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> sourceItems, int pageSize, int pageNumber)
        {
            var totalCountItems = sourceItems.Count();
            var itemsToReturn = sourceItems
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            return new PagedList<T>(itemsToReturn, totalCountItems, pageNumber, pageSize);
        }
    }
}