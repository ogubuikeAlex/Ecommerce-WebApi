using AutoMapper;
using KingsStoreApi.Data.Interfaces;
using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.ProductServiceDTO;
using KingsStoreApi.Model.DataTransferObjects.SharedDTO;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KingsStoreApi.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRepository<Product> _repository;

        public ProductService(IMapper mapper, UserManager<User> userManager, IUnitOfWork unitOfWork, IAuthenticationManager authenticationManager, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _authenticationManager = authenticationManager;
            _signInManager = signInManager;
            _repository = _unitOfWork.GetRepository<Product>();
        }

        public async Task<ReturnModel> UploadProductImage(UploadImageDTO model, User user)
        {
            var product = _repository.GetSingleByCondition(p => p.Id.ToString() == model.UniqueIdentifier && p.UserId == user.Id);

            if (product is null)
                return new ReturnModel { Message = "Product not Found or Current logged in user does not own this product", Success = false };
            
            using (var memoryStream = new MemoryStream())
            {
                await model.File.CopyToAsync(memoryStream);
                product.ProductImage = memoryStream.ToArray();
                await _unitOfWork.SaveChangesAsync();
            }

            return new ReturnModel { Message = "Product Image Uploaded Successfully", Success = true };
        }

        public async Task<ReturnModel> EditProductPrice(EditProductDTO model, User user)
        {
            var product = _repository.GetSingleByCondition(p => p.Id.ToString() == model.Id && p.UserId == user.Id);

            if (product is null)
                return new ReturnModel { Message = "Product not Found or Current logged in user does not own this product", Success = false };

            product.Price = model.NewValue;
            await _unitOfWork.SaveChangesAsync();

            return new ReturnModel { Message = "Price Updated Successfully", Success = true };
        }

        public async Task<ReturnModel> EditProductSummary(EditProductDTO model, User user)
        {
            var product = _repository.GetSingleByCondition(p => p.Id.ToString() == model.Id && p.UserId == user.Id);

            if (product is null)
                return new ReturnModel { Message = "Product not Found or Current logged in user does not own this product", Success = false };

            product.Summary = model.NewValue;
            await _unitOfWork.SaveChangesAsync();

            return new ReturnModel { Message = "Product Summary Updated Successfully", Success = true };
        }

        public async Task<ReturnModel> EditProductTitle(EditProductDTO model, User user)
        {
            var product = _repository.GetSingleByCondition(p => p.Id.ToString() == model.Id && p.UserId == user.Id);

            if (product is null)
                return new ReturnModel { Message = "Product not Found or Current logged in user does not own this product", Success = false };

            product.Title = model.NewValue;
            await _unitOfWork.SaveChangesAsync();

            return new ReturnModel { Message = "Product Title Updated Successfully", Success = true };
        }

        public ReturnModel GetAllProducts()
        {
            var products = _repository.GetAllByCondition();

            if (products is null)
                return new ReturnModel { Success = false, Message = "No product found in store" };

            return new ReturnModel { Success = true, Object = products };
        }

        public ReturnModel GetProductById(string id)
        {
            var product = _repository.GetSingleByCondition(p => p.Id == Guid.Parse(id));

            if (product is null)
                return new ReturnModel { Success = false, Message = "Product not found" };

            return new ReturnModel { Success = true, Object = product };
        }

        public ReturnModel GetProductByName(string name)
        {
            var products = _repository.GetAllByCondition(p => p.Title == name);

            if (products is null)
                return new ReturnModel { Success = false, Message = "No product in our store has that title" };

            return new ReturnModel { Success = true, Object = products };
        }

        public async Task<ReturnModel> GetProductsByVendor(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                return new ReturnModel { Message = "User not found", Success = false };

            if (!user.isVendor)
                return new ReturnModel { Message = "This user is not a vendor", Success = false };

            var products = _repository.GetAllByCondition(p => p.UserId == user.Id && !p.IsDeleted).ToList();

            if (products is null)
                return new ReturnModel { Message = "This vendor has not uploaded any product yet", Success = false };

            return new ReturnModel { Message = "Successful", Object = products, Success = true };
        }

        public async Task<ReturnModel> GetDisabledProductsByVendor(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                return new ReturnModel { Message = "User not found", Success = false };

            if (!user.isVendor)
                return new ReturnModel { Message = "This user is not a vendor", Success = false };

            var products = _repository.GetAllByCondition(p => p.UserId == user.Id && p.IsDeleted).ToList();

            if (products is null)
                return new ReturnModel { Message = "This vendor does not have any disabled products yet", Success = false };

            return new ReturnModel { Message = "Successful", Object = products, Success = true };
        }

        public async Task<ReturnModel> BuyNow ()
        {
            return new ReturnModel { };
        }
        //Search
        //Filter
        //Pagination       
        public async Task<ReturnModel> TemporarilyDisableAProduct(string id)
        {
            var product = _repository.GetSingleByCondition(p => p.Id.ToString() == id);

            if (product is null)
                return new ReturnModel { Message = "Product not found", Success = false };

            if (product.IsDeleted)
                return new ReturnModel { Message = "This Produuct has already been Temporarily diasbled", Success = false };

            var isDeleted = await _repository.ToggleSoftDeleteAsync(product);

            if (!isDeleted)
                return new ReturnModel { Message = "Product Disabling failed", Success = false };

            return new ReturnModel { Message = $"Product\nName:{product.Title}\nTitle: {product.Id}\n has been deleted" };
        }

        public ReturnModel UploadProduct(UploadProductDTO model)
        {
            var product = _mapper.Map<Product>(model);
            product.CreatedAt = DateTime.Now;
            product.PublishedAt = DateTime.Now;
        }
    }
}
