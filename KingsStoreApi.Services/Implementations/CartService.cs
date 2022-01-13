using KingsStoreApi.Data.Interfaces;
using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.CartServiceDTO;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KingsStoreApi.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork unitOfWork;
        private IRepository<Cart> _repository;
        private IRepository<CartItem> _cartItemRepository;

        public CartService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Cart>();
            _cartItemRepository = unitOfWork.GetRepository<CartItem>();
        }
        /*private List<CartItem> _CartContent;
        
        public CartService()
        {
            _CartContent = new List<CartItem>();
        }*/
        public void CreateCart()
        {

        }
        public async Task<ReturnModel> AddCartItem(AddToCartDTO model)
        {
            var cartItem = _cartItemRepository.GetSingleByCondition(p => p.Product.Id == model.Product.Id && p.CartId == model.Cart.Id.ToString());

            if (cartItem is not null)
            {
                cartItem.Quantity++;
                await _cartItemRepository.UpdateAsync();
                return new ReturnModel { Success = false, Message = "Cart item Quantity increased" };
            }

            var newCartItem = new CartItem
            {
                Product = model.Product,
                Quantity = model.Quantity,
                CartId = model.Cart.Id.ToString(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _cartItemRepository.AddAsync(newCartItem);

            return new ReturnModel { Success = true, Message = "Cart Item Added", Object = cartItem };

        }

        public async Task<ReturnModel> RemoveCartItem(string cartItemId)
        {
            var cartItem = _cartItemRepository.GetSingleByCondition(c => c.Id.ToString() == cartItemId);

            if (cartItem is null)
                return new ReturnModel { Success = false, Message = "Cart Item not found" };

            cartItem.IsDeleted = true;
            await _cartItemRepository.UpdateAsync();
            return new ReturnModel { Message = "CartItem removed ", Success = true };
        }

        public void ClearCart(Cart cart)
        {
            var cartItems = _cartItemRepository.GetAllByCondition(c => c.CartId == cart.Id.ToString()).ToList();

            foreach (var item in cartItems)
            {
                item.IsDeleted = true;
                await _cartItemRepository.UpdateAsync;
            }
        }
        public decimal GetTotalCartPrice()
        {
            decimal totalPrice = 0;

            /* foreach (var item in _CartContent)
            {
                item.Product.Price += totalPrice;
            }*/

            return totalPrice;
        }

    }
}