using Shop.DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repository.IRepositories
{
    public interface ILinqOpeations
    {
        Task<LinqOperationResultDto> Some_Linq_operation();
        Task<LinqOperationResultDto> where_filter_operation();
        Task<LinqOperationResultDto> Join_operations();
        Task<bool> UpdateCartAsync(int cartItem,CartDto updateCartItem);
        Task<bool> AddCartAsync(CartDto newCartDto);
        Task<bool> DeleteCartAsync(int cartId);
        Task<List<ProductDto1>> FilterProductsAsync(CardDto card);
    }  
}
 