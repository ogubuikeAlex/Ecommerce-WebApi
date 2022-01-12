using KingsStoreApi.Model.DataTransferObjects.UserServiceDTO;
using System.Threading.Tasks;

namespace KingsStoreApi.Data.Interfaces
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(ValidateUserDTO credential);
        Task<string> CreateToken();
    }
}
