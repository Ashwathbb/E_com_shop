using System.Data;
using AutoMapper;
using Dapper;
using Shop.DataAccess.DTOs;
using Shop.DataAccess.Models;

namespace Shop.Repository.IRepositories.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;
        public ProductRepository(IDbConnection dbConnection, IMapper mapper)
        {
            _dbConnection = dbConnection;
            _mapper = mapper;
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            var sql = "SELECT * FROM Product";
            return _dbConnection.Query<ProductDto>(sql);
        }
        public IEnumerable<ProductDto1> GetProducts()
        {
            var sql = "SELECT * FROM Product";
            // Retrieve data from the database using Dapper
            var products = _dbConnection.Query<ProductDto1>(sql);

            // Map the results to ProductDto1 using AutoMapper
            var productDtos = _mapper.Map<List<ProductDto1>>(products);

            return productDtos;
        }

        public ProductDto GetProductById(int id)
        {
            var sql = "SELECT * FROM Product WHERE ProductId = @Id";
            return _dbConnection.QuerySingleOrDefault<ProductDto>(sql, new { Id = id });
        }

        public void AddProduct(ProductDto entity)
        {
            var sql = "INSERT INTO Product (Name, Price) VALUES (@Name, @Price)";
            _dbConnection.Execute(sql, entity);
        }

        public void UpdateProduct(ProductDto1 entity)
        {
            var sql = "UPDATE Product SET Name = @Name,Price = @Price,ProductSku=@ProductSku,ProductShortName=@ProductShortName,ProductDescription=@ProductDescription,DeliveryTimeSpan=@DeliveryTimeSpan,ProductImageUrl=@ProductImageUrl WHERE ProductId = @ProductId";
            _dbConnection.Execute(sql, entity);
        }

        public void DeleteProduct(int id)
        {
            var sql = "DELETE FROM Product WHERE ProductId = @Id";
            _dbConnection.Execute(sql, new { Id = id });
        }
        // Map entities to DTOs
      
    }
}
