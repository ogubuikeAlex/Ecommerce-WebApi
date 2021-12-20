using KingsStoreApi.Data.Implementations;
using KingsStoreApi.Extensions;
using KingsStoreApi.Model.DataTransferObjects.UserServiceDTO;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingsStoreApi.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly AuthenticationManager _authenticationManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;

        public UserController(UserManager<User> userManager, AuthenticationManager authenticationManager, SignInManager<User> signInManager, IUserService service)
        {
            _userManager = userManager;
            _authenticationManager = authenticationManager;
            _signInManager = signInManager;
           _userService = service;
        }

        public async Task<IActionResult> GetUser(string email)
        {
            var result = await _userService.GetUserAsync(email);

            if (!result.Success)
                return NotFound(result.Message);
            var user = result.Object as User;

            return Ok(user);
        }

        public IActionResult GetAllUsers()
        {
            var result = _userService.GetAllUsers();

            if (!result.Success)
                return NotFound(result.Message);

            var users = result.Object as IEnumerable<User>;
            return Ok(users);
        }

        public async Task<IActionResult> Login(LogInDTO model)
        {
            var result = await _userService.LogIn(model);
            if (!result.Success)
            {
                return result.Message.Contains("User Not found") ? 
                    NotFound(result.Message) : Unauthorized(result.Message);
            }

            var token = result.Object as string;
            return Ok(token);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }

        public async Task<IActionResult> Register(RegisterDTO model)
        {
            var registrationResult = await _userService.RegisterAsync(model);

            if (!registrationResult.Success)
                return BadRequest(registrationResult.Message);

            return Ok(registrationResult.Message);
        }

        public async Task<IActionResult> MakeUserAVendorAsync(string email)
        {
            var result = await _userService.MakeUserAVendorAsync(email);

            if (!result.Success)
            {
                return result.Message.Contains("not found") ?
                    NotFound(result.Message) : BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        public async Task<IActionResult> ToggleUserActivationStatusAsync(string email)
        {
            var result = await _userService.ToggleUserActivationStatusAsync(email);
            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        } 

        public async Task<IActionResult> ToggleUserSoftDeleteAsync (string email)
        {
            var result = await _userService.ToggleUserSoftDeleteAsync(email);

            return Ok(result.Message); 
        }

    }
}
