using System.Data;
using Dapper;
using Shop.DataAccess.DTOs;

namespace Shop.Repository.IRepositories.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDbConnection _dbConnection;

        public CartRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<CartDto> GetAllCarts()
        {
            var sql = "SELECT * FROM Cart";
            return _dbConnection.Query<CartDto>(sql);
        }

        public CartDto GetCartById(int id)
        {
            var sql = "SELECT * FROM Cart WHERE CartId = @Id";
            return _dbConnection.QuerySingleOrDefault<CartDto>(sql, new { Id = id });
        }

        public void AddCart(CartDto entity)
        {
            var sql = "INSERT INTO Cart (UserId, ProductId, Quantity) VALUES (@UserId, @ProductId, @Quantity)";
            _dbConnection.Execute(sql, entity);
        }

        public void UpdateCart(CartDto entity)
        {
            var sql = "UPDATE Cart SET UserId = @UserId, ProductId = @ProductId, Quantity = @Quantity WHERE CartId = @CartId";
            _dbConnection.Execute(sql, entity);
        }

        public void DeleteCart(int id)
        {
            var sql = "DELETE FROM Cart WHERE CartId = @Id";
            _dbConnection.Execute(sql, new { Id = id });
        }
    }
}
