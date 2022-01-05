using KingsStoreApi.Model.Entities;

namespace KingsStoreApi.Model.DataTransferObjects.UserServiceDTO
{
    public class UpdateFullNameDTO
    {
        public User User { get; set; }
        public string NewFullName { get; set; }
    }
}
