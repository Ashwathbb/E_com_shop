using Shop.DataAccess.DTOs;
using Shop.Repository.IRepositories;

namespace Shop.Service.IService.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public ProductDto GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }

        public void CreateProduct(ProductDto productDto)
        {
            _productRepository.AddProduct(productDto);
        }

        public void UpdateProduct(ProductDto productDto)
        {
            _productRepository.UpdateProduct(productDto);
        }

        public void DeleteProduct(int id)
        {
            _productRepository.DeleteProduct(id);
        }
    }
}
