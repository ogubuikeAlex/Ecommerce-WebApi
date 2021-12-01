using KingsStoreApi.Helpers.Implementations;

namespace KingsStoreApi.Services.Interfaces
{
    public interface IUserService
    {
        ReturnModel GetUser(string email);
        ReturnModel GetAllUsers();
        ReturnModel LogIn(LogInDTO model);
        ReturnModel LogOut(LogOutDTO model);
        ReturnModel Register (RegisterDTO model);
        ReturnModel AddVendor (RegisterDTO model);
        ReturnModel UpdateUser (UpdateDTO model);
        ReturnModel UploadProfilePicture(UploadProfilePictureDTO model);
        ReturnModel RemoveProfilePicture(UploadProfilePictureDTO model);
        ReturnModel ToggleUserActivationStatus (ToggleUserActivationDTO model);
        ReturnModel SoftDeleteUser (DeleteDTO model);
    }
}
