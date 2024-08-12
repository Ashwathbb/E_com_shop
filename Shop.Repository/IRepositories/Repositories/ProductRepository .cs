using System.Data;
using Dapper;
using Shop.DataAccess.DTOs;

namespace Shop.Repository.IRepositories.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProductRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            var sql = "SELECT * FROM Product";
            return _dbConnection.Query<ProductDto>(sql);
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

        public void UpdateProduct(ProductDto entity)
        {
            var sql = "UPDATE Product SET Name = @Name, Price = @Price WHERE ProductId = @ProductId";
            _dbConnection.Execute(sql, entity);
        }

        public void DeleteProduct(int id)
        {
            var sql = "DELETE FROM Product WHERE ProductId = @Id";
            _dbConnection.Execute(sql, new { Id = id });
        }
    }
}
