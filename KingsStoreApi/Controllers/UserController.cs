using KingsStoreApi.Data.Implementations;
using KingsStoreApi.Data.Interfaces;
using KingsStoreApi.Extensions;
using KingsStoreApi.Model.DataTransferObjects.SharedDTO;
using KingsStoreApi.Model.DataTransferObjects.UserServiceDTO;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IAuthenticationManager _authenticationManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _environment;

        public UserController(UserManager<User> userManager, IAuthenticationManager authenticationManager, SignInManager<User> signInManager, IUserService service, IWebHostEnvironment webHostEnvironment)
        {
            _environment = webHostEnvironment;
            _userManager = userManager;
            _authenticationManager = authenticationManager;
            _signInManager = signInManager;
            _userService = service;
        }

        [HttpGet("{email}")] //working
        public async Task<IActionResult> GetUser(string email)
        {
            var result = await _userService.GetUserAsync(email);

            if (!result.Success)
                return NotFound(result.Message);
            var user = result.Object as User;

            return Ok(user);
        }

        [HttpGet] //working
        public IActionResult GetAllUsers()
        {
            var result = _userService.GetAllUsers();

            if (!result.Success)
                return NotFound(result.Message);

            var users = result.Object as IEnumerable<User>;
            return Ok(users);
        }
        [HttpGet("vendors")] //working
        public IActionResult GetAllVendors()
        {
            var result = _userService.GetAllVendors();

            if (!result.Success)
                return NotFound(result.Message);
            var vendors = result.Object as IEnumerable<User>;

            return Ok(vendors);
        }
        [HttpGet("activeUsers")] //working
        public IActionResult GetAllActiveUsers()
        {
            var result = _userService.GetAllActiveUsers();

            if (!result.Success)
                return NotFound(result.Message);
            var users = result.Object as IEnumerable<User>;

            return Ok(users);
        }
        [HttpGet("customers")] //working
        public IActionResult GetAllCustomers()
        {
            var result = _userService.GetAllCustomers();

            if (!result.Success)
                return NotFound(result.Message);
            var users = result.Object as IEnumerable<User>;

            return Ok(users);
        }
        [HttpPost("removeVendor")]////working
        public async Task<IActionResult> UnmakeUserAVendorAsync(string email)
        {
            var result = await _userService.UnMakeUserAVendorAsync(email);

            if (!result.Success)
                return result.Message.Contains("not found") ?
                    NotFound(result.Message) : BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("makeAdmin")] //working
        public async Task<IActionResult> MakeUserAnAdminAsync(string email)
        {
            var result = await _userService.MakeUserAnAdminAsync(email);

            if (!result.Success)
                return result.Message.Contains("not found") ?
                     NotFound(result.Message) : BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("removeAdmin")]//working
        public async Task<IActionResult> UnmakeUserAnAdminAsync(string email)
        {
            var result = await _userService.UnMakeUserAnAdminAsync(email);

            if (!result.Success)
                return result.Message.Contains("not found") ?
                    NotFound(result.Message) : BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("updatePic")]//working
        public async Task<IActionResult> UpdateUserProfilePic([FromForm]UploadImageDTO model)
        {
            var result = await _userService.UpdateUserProfilePic(model);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("updateBio"), Authorize] //Working
        public async Task<IActionResult> UpdateUserBio(string newBio)
        {            
            var user = await GetLoggedInUserAsync();
            var result = await _userService.UpdateUserBio(user, newBio);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("updateName"), Authorize]//working
        public async Task<IActionResult> UpdateUserFullName(string newName)
        {
            var user = await GetLoggedInUserAsync();

            var result = await _userService.UpdateUserFullName(user, newName);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("removePic"), Authorize]//working
        public async Task<IActionResult> RemoveUserProfilePicture()
        {
            var user = await GetLoggedInUserAsync();
            var result = await _userService.RemoveProfilePicture(user);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost] //working
        public async Task<IActionResult> Login(LogInDTO model)
        {
            var result = await _userService.LogIn(model);
            if (!result.Success)
            {
                return result.Message.Contains("User Not found") ?
                    NotFound(result.Message) : Unauthorized(result.Message);
            }

            var token = result.Object as string;
            return Ok($"{result.Message}\nToken: {token}");
        }

        [HttpPost("logout")] //working
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }

        [HttpPost("register")] //working
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            var registrationResult = await _userService.RegisterAsync(model);

            if (!registrationResult.Success)
                return BadRequest(registrationResult.Message);

            return Ok(registrationResult.Message);
        }

        [HttpPost("makeVendor")] //working
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

        [HttpPost("toggleActiveStatus")]//working
        public async Task<IActionResult> ToggleUserActivationStatusAsync(string email)
        {
            var result = await _userService.ToggleUserActivationStatusAsync(email);
            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("toggleSoftDelete")]//working
        public async Task<IActionResult> ToggleUserSoftDeleteAsync(string email)
        {
            var result = await _userService.ToggleUserSoftDeleteAsync(email);

            return Ok(result.Message);
        }

        private async Task<User> GetLoggedInUserAsync()
        {
            var (userId, userEmail) = HttpContext.User.GetLoggedInUserInfo();
            var user = await _userManager.FindByNameAsync(userEmail);
            return user;
        }
    }
}