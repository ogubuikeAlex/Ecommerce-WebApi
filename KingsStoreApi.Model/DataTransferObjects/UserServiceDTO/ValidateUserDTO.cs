using KingsStoreApi.Model.Entities;

namespace KingsStoreApi.Model.DataTransferObjects.UserServiceDTO
{
    public class ValidateUserDTO
    {
        public User User { get; set; }
        public string Password { get; set; }
    }
}
