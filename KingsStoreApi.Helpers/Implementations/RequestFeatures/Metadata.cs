namespace KingsStoreApi.Helpers.Implementations.RequestFeatures
{
    public class Metadata
    {
        /// <summary>
        /// CurrentPage: page we are on
        /// TotalPages: after dividing totalcount by page size
        /// TotalCount: Number of all our resourses
        /// PageSize: how many resources we want on a page
        /// </summary>
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
