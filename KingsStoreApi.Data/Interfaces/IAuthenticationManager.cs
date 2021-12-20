using KingsStoreApi.Model.DataTransferObjects.UserServiceDTO;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KingsStoreApi.Data.Interfaces
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(LogInDTO credential);
        Task<string> CreateToken();
    }
}
