namespace KingsStoreApi.Model.Enums
{
    public enum OrderStatus
    {
        New = 1,
        Checkout,
        Paid,
        Failed,
        Shipped,
        Delivered,
        Returned,
        Complete
    }
}
