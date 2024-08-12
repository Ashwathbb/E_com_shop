using Shop.DataAccess.DTOs;

namespace Shop.Service.IService
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAllProducts();
        ProductDto GetProductById(int id);
        void CreateProduct(ProductDto productDto);
        void UpdateProduct(ProductDto productDto);
        void DeleteProduct(int id);
    }
}
