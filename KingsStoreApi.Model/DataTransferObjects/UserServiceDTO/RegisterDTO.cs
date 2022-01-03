using System;

namespace KingsStoreApi.Model.DataTransferObjects.UserServiceDTO
{
    public class RegisterDTO
    {
        public string FullName { get; set; }
        public string Bio { get; set; }
        public  string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
