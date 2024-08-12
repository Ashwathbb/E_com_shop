using Dapper;
using Shop.DataAccess.DTOs;
using System.Data;

namespace Shop.Repository.IRepositories.Repositories
{
    public class CountryRepository : ICountryRepository
    { 
        private readonly IDbConnection _dbConnection;

        public CountryRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<CountryDto> GetAllCountries()
        {
            var response = _dbConnection.Query<CountryDto>("SELECT * FROM Country");

            return response.ToList();
        }

        public CountryDto GetCountryById(int id)
        {
            var response = _dbConnection.QuerySingleOrDefault<CountryDto>("SELECT * FROM Country WHERE CountryId = @Id", new { Id = id });

            if (response == null)
            {
                return null;
            }
            return response;
        }

        public void AddCountry(CountryDto entity)
        {
            var sql = "INSERT INTO Country (Name) VALUES (@Name)";
            _dbConnection.Execute(sql, entity);
        }

        public void UpdateCountry(CountryDto entity)
        {
            var sql = "UPDATE Country SET Name = @Name WHERE CountryId = @CountryId";
            _dbConnection.Execute(sql, entity);
        }

        public void DeleteCountry(int id)
        {
            var sql = "DELETE FROM Country WHERE CountryId = @Id";
            _dbConnection.Execute(sql, new { Id = id });
        }
    }
}
