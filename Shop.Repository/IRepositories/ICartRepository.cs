using Shop.DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repository.IRepositories
{
    public interface ICartRepository
    {
        IEnumerable<CartDto> GetAllCarts();
        CartDto GetCartById(int id);
        void AddCart(CartDto entity);
        void UpdateCart(CartDto entity);
        void DeleteCart(int id);
    }
}
