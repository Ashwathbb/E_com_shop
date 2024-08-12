using Shop.DataAccess.DTOs;
using Shop.Repository.IRepositories;

namespace Shop.Service.IService.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public IEnumerable<CartDto> GetAllCarts()
        {
            return _cartRepository.GetAllCarts();
        }

        public CartDto GetCartById(int id)
        {
            return _cartRepository.GetCartById(id);
        }

        public void CreateCart(CartDto cartDto)
        {
            _cartRepository.AddCart(cartDto);
        }

        public void UpdateCart(CartDto cartDto)
        {
            _cartRepository.UpdateCart(cartDto);
        }

        public void DeleteCart(int id)
        {
            _cartRepository.DeleteCart(id);
        }
    }

}
