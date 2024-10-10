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
        IEnumerable<ProductDto1> GetProducts();
        ProductDto GetProductById(int id);
        void AddProduct(ProductDto entity);
        void UpdateProduct(ProductDto1 entity);
        void DeleteProduct(int id);
    }
}
