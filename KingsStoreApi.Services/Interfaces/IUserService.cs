using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.SharedDTO;
using KingsStoreApi.Model.DataTransferObjects.UserServiceDTO;
using System.Threading.Tasks;

namespace KingsStoreApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<ReturnModel> GetUserAsync(string email);
        ReturnModel GetAllUsers();
        ReturnModel GetAllVendors();
        ReturnModel GetAllActiveUsers();
        //Explore usermanger
        //Explore signinmanager//
        Task<ReturnModel> LogIn(LogInDTO model);
        Task<ReturnModel> LogOut();
        Task<ReturnModel> RegisterAsync (RegisterDTO model);
        Task<ReturnModel> MakeUserAVendorAsync(string email);
        Task<ReturnModel> UnMakeUserAVendorAsync(string email);
        Task<ReturnModel> MakeUserAnAdminAsync(string email);
        Task<ReturnModel> UnMakeUserAnAdminAsync(string email);
        Task<ReturnModel> UpdateUserProfilePic(UploadImageDTO model);
        Task<ReturnModel> UpdateUserBio(string email);
        Task<ReturnModel> UpdateUserFullName(UpdateFullNameDTO model);
        Task<ReturnModel> RemoveProfilePicture(string email);
        Task<ReturnModel> ToggleUserActivationStatusAsync(string email);
        Task<ReturnModel> ToggleUserSoftDeleteAsync (string email);
    }
}
