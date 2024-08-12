using Shop.DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repository.IRepositories
{
    public interface IProductRepository
    {
        IEnumerable<ProductDto> GetAllProducts();
        ProductDto GetProductById(int id);
        void AddProduct(ProductDto entity);
        void UpdateProduct(ProductDto entity);
        void DeleteProduct(int id);
    }
}
