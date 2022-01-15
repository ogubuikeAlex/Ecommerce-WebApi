using AutoMapper;
using KingsStoreApi.Data.Interfaces;
using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.SharedDTO;
using KingsStoreApi.Model.DataTransferObjects.UserServiceDTO;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Model.Enums;
using KingsStoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KingsStoreApi.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRepository<User> _repository;
        private readonly IRepository<Cart> _cartrepository;
        private User _user;

        public UserService(IServiceFactory serviceFactory, IMapper mapper, UserManager<User> userManager,
            IUnitOfWork unitOfWork,
           SignInManager<User> signInManager)
        {
            _serviceFactory = serviceFactory;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _repository = unitOfWork.GetRepository<User>();
            _cartrepository = unitOfWork.GetRepository<Cart>();
        }
        public async Task<ReturnModel> MakeUserAVendorAsync(string email)
        {
            var user = await _userManager.FindByNameAsync(email);

            if (user is null)
                return new ReturnModel { Message = $"User : {email} is not found\nTry to register first! ", Success = false };

            if (user.isVendor)
                return new ReturnModel { Message = $"User : {user.FullName} is already a vendor", Success = false };

            user.isVendor = true;

            var result = await _userManager.AddToRoleAsync(user, Roles.Vendor.ToString());

            if (!result.Succeeded)
                return new ReturnModel { Message = $"Vendor role not added to User : {user.FullName}\n {result.Errors.FirstOrDefault().Description}", Success = false };/*
*/
            await _userManager.UpdateAsync(user);

            return new ReturnModel { Success = true, Message = $"User : {user.FullName} is now a vendor" };
        }

        public ReturnModel GetAllUsers()
        {
            var users = _userManager.Users.ToList();

            if (users.Count < 1)
                return new ReturnModel { Success = false, Message = "No users are on this system yet" };

            return new ReturnModel { Success = true, Object = users };
        }

        public ReturnModel GetAllVendors()
        {
            var users = _repository.GetAllByCondition(u => u.isVendor).ToList();

            if (users.Count < 1)
                return new ReturnModel { Success = false, Message = "No vendors are on this system yet" };

            return new ReturnModel { Success = true, Object = users };
        }
        public ReturnModel GetAllCustomers()
        {
            var users = _repository.GetAllByCondition(u => !u.isVendor && !u.isAdmin).ToList();

            if (users.Count < 1)
                return new ReturnModel { Success = false, Message = "No customers are on this system yet" };

            return new ReturnModel { Success = true, Object = users };
        }

        public ReturnModel GetAllActiveUsers()
        {
            var users = _repository.GetAllByCondition(u => u.isActive).ToList();

            if (users.Count < 1)
                return new ReturnModel { Success = false, Message = "No active users are on this system yet" };

            return new ReturnModel { Success = true, Object = users };
        }

        public async Task<ReturnModel> GetUserAsync(string email)
        {
            var user = await _userManager.FindByNameAsync(email);

            if (user is null)
                return new ReturnModel { Message = "User not found!", Success = false };

            return new ReturnModel { Message = "User found", Success = true, Object = user };
        }

        public async Task<ReturnModel> LogIn(LogInDTO model)
        {
            var authenticationManager = _serviceFactory.GetServices<IAuthenticationManager>();
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user is null)
                return new ReturnModel { Message = "User Not found please register to continue", Success = false };

            var validateUserDTO = new ValidateUserDTO { User = user, Password = model.Password };

            var isUserValid = await authenticationManager.ValidateUser(validateUserDTO);

            if (!isUserValid)
                return new ReturnModel { Message = "Invalid email or password", Success = false };
            await _signInManager.SignInAsync(user, false);
            var token = await authenticationManager.CreateToken();
            _user = user;
            return new ReturnModel { Message = "Your login was successful", Success = true, Object = token };
        }

        public async Task<ReturnModel> LogOut()
        {
            await _signInManager.SignOutAsync();
            return new ReturnModel { Message = "User signed out" };
        }

        public async Task<ReturnModel> RegisterAsync(RegisterDTO model)
        {
            var newUser = _mapper.Map<User>(model);

            newUser.LastLogin = DateTime.Now;
            newUser.CreatedAt = DateTime.Now;
            newUser.UpdatedAt = DateTime.Now;
            newUser.isActive = true;
            newUser.UserName = model.Email;

            var cart = new Cart
            {
                CartStatus = CartStatus.Empty,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = newUser.Id                
            };
            await _cartrepository.AddAsync(cart);

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
                return new ReturnModel { Message = "Create User failed", Success = false };

            result = await _userManager.AddToRoleAsync(newUser, Roles.Customer.ToString());

            if (!result.Succeeded)
                return new ReturnModel { Message = "Role Assignment failed", Success = false };

            return new ReturnModel { Message = $"{ newUser.FullName } Added successfully", Success = true };
        }

        public async Task<ReturnModel> RemoveProfilePicture(User user)
        {
            user.ProfilePicture = null;
            await _userManager.UpdateAsync(user);

            return new ReturnModel { Message = "Profile pic successfully removed", Success = true };
        }

        public async Task<ReturnModel> ToggleUserSoftDeleteAsync(string email)
        {
            var result = await GetUserAsync(email);

            var user = result.Object as User;
            var isDeleted = await _repository.ToggleSoftDeleteAsync(user);
            await _userManager.UpdateAsync(user);

            if (isDeleted)
                return new ReturnModel { Message = $"User {user.Email} has been deleted", Success = true };

            return new ReturnModel { Message = $"User {user.Email} has been restored", Success = false };

        }

        public async Task<ReturnModel> ToggleUserActivationStatusAsync(string email)
        {
            var result = await GetUserAsync(email);

            if (!result.Success)
                return new ReturnModel { Message = "User not found, Action failed!", Success = false };

            var user = result.Object as User;

            if (user.isActive)
            {
                user.isActive = false;
                return new ReturnModel { Message = $"User: {email} has been deactivated", Success = true };
            }

            user.isActive = true;
            return new ReturnModel { Message = $"User: {email} has been Activated", Success = true };
        }

        public async Task<ReturnModel> UnMakeUserAVendorAsync(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user is null)
                return new ReturnModel { Message = "User not found", Success = false };

            if (!user.isVendor)
                return new ReturnModel { Message = $"User : {user.FullName} is Not a vendor", Success = false };

            var result = await _userManager.RemoveFromRoleAsync(user, Roles.Vendor.ToString());

            if (!result.Succeeded)
                return new ReturnModel { Message = result.Errors.FirstOrDefault().Description, Success = false };

            user.isVendor = false;
            await _userManager.UpdateAsync(user);

            return new ReturnModel { Message = $"User: {user.FullName} is no longer a vendor", Success = true, Object = user };
        }

        public async Task<ReturnModel> MakeUserAnAdminAsync(string email)
        {
            var user = await _userManager.FindByNameAsync(email);

            if (user is null)
                return new ReturnModel { Message = $"User : {email} is not found\nTry to register first! ", Success = false };

            if (user.isAdmin)
                return new ReturnModel { Message = $"User : {user.FullName} is already an admmin", Success = false };

            user.isAdmin = true;
            var result = await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());

            if (!result.Succeeded)
                return new ReturnModel { Message = $"Admin role not added to User : {user.FullName}", Success = false };

            await _userManager.UpdateAsync(user);

            return new ReturnModel { Message = $"User: {user.FullName} is now an Admin", Success = true, Object = user };
        }

        public async Task<ReturnModel> UnMakeUserAnAdminAsync(string email)
        {
            var user = await _userManager.FindByNameAsync(email);

            if (user is null)
                return new ReturnModel { Message = $"User : {email} is not found\nTry to register first! ", Success = false };

            if (!user.isAdmin)
                return new ReturnModel { Message = $"User : {user.FullName} is Not an admin", Success = false };

            var result = await _userManager.RemoveFromRoleAsync(user, Roles.Admin.ToString());

            if (!result.Succeeded)
                return new ReturnModel { Message = result.Errors.FirstOrDefault().Description, Success = false };

            user.isAdmin = false;
            await _userManager.UpdateAsync(user);
            return new ReturnModel { Message = $"User: {user.FullName} is no longer an Admin", Success = true, Object = user };
        }

        public async Task<ReturnModel> UpdateUserProfilePic(UploadImageDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.UniqueIdentifier);
            IdentityResult result;

            using (var memoryStream = new MemoryStream())
            {
                await model.File.CopyToAsync(memoryStream);
                user.ProfilePicture = memoryStream.ToArray();
                result = await _userManager.UpdateAsync(user);
            }

            if (!result.Succeeded)
                return new ReturnModel { Message = result.Errors.FirstOrDefault().ToString(), Success = false };

            return new ReturnModel { Message = "Profile image added Successfully", Object = user, Success = true };
        }

        public async Task<ReturnModel> UpdateUserBio(User user, string newBio)
        {
            user.Bio = newBio;
            await _userManager.UpdateAsync(user);

            return new ReturnModel { Message = $"User: Bio Update Successful", Success = true, Object = user };
        }

        public async Task<ReturnModel> UpdateUserFullName(User user, string newName)
        {
            user.FullName = newName;
            await _userManager.UpdateAsync(user);

            return new ReturnModel { Message = $"User: Name updated", Success = true, Object = user };
        }
    }
}


