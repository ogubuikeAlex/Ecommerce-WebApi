using KingsStoreApi.Data.Interfaces;
using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KingsStoreApi.Services.Implementations
{
    public class CartService : ICartService
    {
        private IRepository<Cart> _repository;
        private IRepository<CartItem> _cartItemRepository;
        private IRepository<Product> _productRepository;

        public CartService(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Cart>();
            _cartItemRepository = unitOfWork.GetRepository<CartItem>();
            _productRepository = unitOfWork.GetRepository<Product>();
        }

        public async Task<ReturnModel> AddCartItem(User user, string productId, int quantity)
        {           
            var product = _productRepository.GetSingleByCondition(a => a.Id.ToString() == productId);
            
            if (product is null)
                return new ReturnModel { Success = false, Message = "Product either does not exist or has been deleted" };

            if (product.UserId == user.Id)
                return new ReturnModel { Message = "You cannot buy your own products", Success = false };

            if (product.Quantity < quantity)
                return new ReturnModel { Message = $"OOPS! Insufficient Products\nWe currently have {product.Quantity} of {product.Title} in our store, " +
                    $"\nyour order exceeds that! \nReduce your order and try again \nor check again in a few days after restock", Success = false };
            
            var cart = _repository.GetSingleByCondition(c => c.UserId == user.Id);

            var cartItem = _cartItemRepository.GetSingleByCondition( p => p.ProductId == productId && p.CartId == cart.Id.ToString());

            if (cartItem is not null)
            {
                cartItem.Quantity += quantity;
                await _cartItemRepository.UpdateDBAsync();
                return new ReturnModel { Success = true, Message = $"Cart item: Quantity increased" };
            }

            var newCartItem = new CartItem
            {
                Product = product,
                ProductId = productId,
                Quantity = quantity,
                CartId = cart.Id.ToString(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _cartItemRepository.AddAsync(newCartItem);

            return new ReturnModel { Success = true, Message = $"{quantity} unit(s) of item Added", Object = cartItem };
        }

        public async Task<ReturnModel> RemoveCartItem(string cartItemId)
        {
            var cartItem = _cartItemRepository.GetSingleByCondition(c => c.Id.ToString() == cartItemId);

            if (cartItem is null)
                return new ReturnModel { Success = false, Message = "Cart Item not found" };

            cartItem.IsDeleted = true;
            await _cartItemRepository.UpdateDBAsync();
            return new ReturnModel { Message = "CartItem removed ", Success = true };
        }

        public async Task<ReturnModel> ClearCart(string userId)
        {
            var cart = _repository.GetSingleByCondition(c => c.UserId == userId);

            var cartItems = _cartItemRepository.GetAllByCondition(c => c.CartId == cart.Id.ToString()).ToList();

            if (cartItems.Count < 1)
                return new ReturnModel { Message = "This cart does not contain any items. No Cart Items found", Success = false };

            foreach (var item in cartItems)
            {
                item.IsDeleted = true;
                await _cartItemRepository.UpdateDBAsync();
            }
            return new ReturnModel { Success = true, Message = "Cart Cleared" };
        }

        public ReturnModel GetTotalCartPrice(string userId)
        {
            //instead of querying again for cart, initialize user with cart with 
            var cart = _repository.GetSingleByCondition(c => c.UserId == userId);

            decimal totalPrice = 0;

            var cartItems = _cartItemRepository.GetAllByCondition(c => c.CartId == cart.Id.ToString(), includeProperties: "Product").ToList();

            if (cartItems.Count < 1)
                return new ReturnModel { Message = "This cart does not contain any items", Success = false };

            foreach (var item in cartItems)
            {
                totalPrice += item.Product.Price;
            }

            return new ReturnModel { Success = true, Message = "price gotten", Object = totalPrice.ToString() };
        }

        public ReturnModel GetCartItems(string userId)
        {
            //instead of querying again for cart, initialize user with cart with 
            var cart = _repository.GetSingleByCondition(c => c.UserId == userId);            

            var cartItems = _cartItemRepository.GetAllByCondition(c => c.CartId == cart.Id.ToString(), includeProperties: "Product").ToList();

            if (cartItems.Count < 1)
                return new ReturnModel { Message = "This cart does not contain any items", Success = false };         

            return new ReturnModel { Success = true, Message = "price gotten", Object = cartItems };
        }
    }
}