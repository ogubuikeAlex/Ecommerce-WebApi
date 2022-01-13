namespace KingsStoreApi.Helpers.Implementations.RequestFeatures
{
    public abstract class RequestParameters
    {
        /// <summary>
        /// MaxPageSize: maximum number of resources returned at a time
        /// PageSize: is the nuber of resources to on the page, by default it is 5 except the requester changes it
        /// PageNumber : is the page to be returnred, by defaulr it is one , excpet the requester changes it 
        /// </summary>
        private const int MaxPageSize = 20;
        private int _pageSize = 5;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = (value > MaxPageSize) ? MaxPageSize : value;

            }
        }
    }
}
