using AutoMapper;
using KingsStoreApi.Data.Interfaces;
using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace KingsStoreApi.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRepository<Product> _repository;

        public ProductService(IMapper mapper, UserManager<User> userManager, IUnitOfWork unitOfWork, IAuthenticationManager authenticationManager, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _authenticationManager = authenticationManager;
            _signInManager = signInManager;
            _repository = unitOfWork.GetRepository<Product>();
        }

        public ReturnModel EditProduct(EditProductDTO model)
        {
            throw new System.NotImplementedException();
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
                return new ReturnModel { Success = false, Message = "Product not found"};

            return new ReturnModel { Success = true, Object = product };
        }

        public ReturnModel GetProductsByName(string name)
        {
            var products = _repository.GetAllByCondition(p => p.Title == name);

            if (products is null)
                return new ReturnModel { Success = false, Message = "No product in our store has that title"};

            return new ReturnModel { Success = true, Object = products };
        }

        public ReturnModel GetProductsByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public ReturnModel RemoveProduct(RemoveProductDTO model)
        {
            throw new System.NotImplementedException();
        }

        public ReturnModel SoftDeleteProduct()
        {
            throw new System.NotImplementedException();
        }

        public ReturnModel UploadProduct(UploadProductDTO model)
        {
            throw new System.NotImplementedException();
        }
    }
}
