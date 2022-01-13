namespace KingsStoreApi.Helpers.Implementations.RequestFeatures
{
    public class ProductRequestParameters : RequestParameters
    {
        public uint MaxPrice => uint.MaxValue;
        public uint MinPrice { get; set; }
        public bool ValidPriceRange => MaxPrice > MinPrice;
    }
}
