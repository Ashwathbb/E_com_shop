using Shop.DataAccess.DTOs;

namespace Shop.Service.IService
{
    public interface ICartService
    {
        IEnumerable<CartDto> GetAllCarts();
        CartDto GetCartById(int id);
        void CreateCart(CartDto cartDto);
        void UpdateCart(CartDto cartDto);
        void DeleteCart(int id);
    }
}
