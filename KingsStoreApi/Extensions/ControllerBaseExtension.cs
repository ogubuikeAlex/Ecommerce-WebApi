using KingsStoreApi.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KingsStoreApi.Extensions
{
    public class ControllerBaseExtension : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public ControllerBaseExtension(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        protected async Task<User> GetLoggedInUserAsync()
        {
            var (userId, userEmail) = HttpContext.User.GetLoggedInUserInfo();
            var user = await _userManager.FindByNameAsync(userEmail);
            return user;
        }
    }
}
