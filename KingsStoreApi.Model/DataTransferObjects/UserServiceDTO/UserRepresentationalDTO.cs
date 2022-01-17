namespace KingsStoreApi.Model.DataTransferObjects.UserServiceDTO
{
    public class UserRepresentationalDTO
    {
        public string FullName { get; set; }
        public bool isAdmin { get; set; }
        public bool isVendor { get; set; }
        public string Bio { get; set; }        
    }
}
