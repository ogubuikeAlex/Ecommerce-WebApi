using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.UserServiceDTO;
using System.Threading.Tasks;

namespace KingsStoreApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<ReturnModel> GetUserAsync(string email);
        ReturnModel GetAllUsers();
        Task<ReturnModel> LogIn(LogInDTO model);
        Task<ReturnModel> LogOut();
        Task<ReturnModel> RegisterAsync (RegisterDTO model);
        Task<ReturnModel> MakeUserAVendorAsync(string email);
        /*ReturnModel UpdateUser (UpdateDTO model);
        ReturnModel UploadProfilePicture(UploadProfilePictureDTO model);*/
        ReturnModel RemoveProfilePicture();
        Task<ReturnModel> ToggleUserActivationStatusAsync(string email);
        Task<ReturnModel> ToggleUserSoftDeleteAsync (string email);
    }
}
