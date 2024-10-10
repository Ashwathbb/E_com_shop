using Shop.DataAccess.DTOs;

namespace Shop.Service.IService
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAllProducts();
        IEnumerable<ProductDto1> GetProducts();
        ProductDto GetProductById(int id);
        void CreateProduct(ProductDto productDto);
        void UpdateProduct(ProductDto1 productDto);
        void DeleteProduct(int id);
    }
}
