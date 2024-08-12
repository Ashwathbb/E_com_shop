using System.Data;
using Dapper;
using Shop.DataAccess.DTOs;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Dept.DataAcess.Dto;
using Shop.DataAccess.Models;
//using Shop.DataAccess.Models;

namespace Shop.Repository.IRepositories.Repositoriescc
{
    public class UsersInfoRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ShopDbContext _shopDbContext;
        public UsersInfoRepository(IDbConnection dbConnection, ShopDbContext shopDbContext)
        {
            _dbConnection = dbConnection;
            _shopDbContext = shopDbContext;
        }

        public async Task<IEnumerable<UsersInfoDto>> GetAllUsersAsync()
        {
            var sql = @"SELECT [UserId]
                   ,[UserName]
                   ,[EmailId]
                   ,[CountryId]
                   ,[Password]
                   ,[IsActive]
                   ,[FailedLoginAttempts]
                   ,[UsersInfoGuid]
                   ,[DepartmentGuid]
               FROM [UsersInfo]";
            var users = await _dbConnection.QueryAsync<UsersInfoDto>(sql);
            return users;
        }

        public async Task<UsersInfoDto> GetUserById(Guid userId)
        {
            var sql = "SELECT * FROM UsersInfo WHERE UsersInfoGuid = @Guid";
            var user = await _dbConnection.QuerySingleOrDefaultAsync<UsersInfoDto>(sql, new { Guid = userId });
            return user;
        }

        public async Task AddUser(CreateUserDto entity)
        {
            try
            {
                var userInfo = new UsersInfo
                {
                    UserName = entity.UserName, // Ensure this is unique and set
                    Password = entity.Password,
                    EmailId = entity.EmailId,
                    IsActive = entity.IsActive,
                    DepartmentGuid = entity.DepartmentGuid

                };
                  _shopDbContext.UsersInfos.Add(userInfo);
                 await _shopDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error occurred while saving customer data.", ex);
            }
        }
        public async Task AddUsersInfoAsync(UsersInfoDepartment usersInfoDto)
        {
            try
            {
                var usersInfo = new UsersInfo
                {
                    UsersInfoGuid = usersInfoDto.UsersInfoGuid,
                    UserName = usersInfoDto.UserName,
                    DepartmentGuid=usersInfoDto.DepartmentGuid
                };
                _shopDbContext.UsersInfos.Add(usersInfo);
                await _shopDbContext.SaveChangesAsync();
            }
            catch(DbUpdateException ex)
            {
                throw new Exception("Error occurred while saving customer data.", ex);
            }
        }
        public async Task AddUserAndDepartmentAsync(UsersInfoDepartment usersInfoDepartment)
        {
            try
            {
                var usersInfo = new UsersInfo
                {
                    UsersInfoGuid = usersInfoDepartment.UsersInfoGuid,
                    UserName = usersInfoDepartment.UserName,
                    DepartmentGuid = usersInfoDepartment.DepartmentGuid
                };
                _shopDbContext.UsersInfos.Add(usersInfo);
                await _shopDbContext.SaveChangesAsync();
            }
            catch(DbUpdateException ex)
            {
                throw new Exception("Error occurred while saving customer data.", ex);
            }
        }
        public void UpdateUser(UsersInfoDto entity)
        {
            var sql = @"UPDATE UsersInfo SET 
                    DepartmentGuid = @DepartmentGuid, 
                    UserName = @UserName, 
                    EmailId = @EmailId, 
                    CountryId = @CountryId, 
                    Password = @Password, 
                    IsActive = @IsActive 
                    WHERE UsersInfoGuid = @UsersInfoGuid";
            _dbConnection.ExecuteAsync(sql, entity);
        }

        public void DeleteUser(int id)
        {
            var sql = "DELETE FROM UsersInfo WHERE UserId = @Id";
            _dbConnection.Execute(sql, new { Id = id });
        }
        public UsersInfoDto GetUserByUsername(string username, string password)
        {
            var sql = "SELECT * FROM UsersInfo WHERE UserName = @Username AND  Password=@Password";
            return _dbConnection.QueryFirstOrDefault<UsersInfoDto>(sql, new { Username = username, Password = password });
        }
        public void AddFailedLoginAttempt(int userId, DateTime attemptTime)
        {
            var sql = "UPDATE UsersInfo SET FailedLoginAttempts = FailedLoginAttempts + 1 WHERE UserId = @UserId";
            _dbConnection.Execute(sql, new { UserId = userId });
        }
        public void UpdateUserLoginAttempts(int userId, int failedLoginAttempts)
        {
            var sql = "UPDATE UsersInfo SET FailedLoginAttempts = @FailedLoginAttempts WHERE UserId = @UserId";
            _dbConnection.Execute(sql, new { UserId = userId, FailedLoginAttempts = failedLoginAttempts });
        }

        public void UpdateUserLoginAttemptsAndBlock(int userId, int failedLoginAttempts, bool isActive)
        {
            var sql = "UPDATE UsersInfo SET FailedLoginAttempts = @FailedLoginAttempts, IsActive = @IsActive WHERE UserId = @UserId";
            _dbConnection.Execute(sql, new { UserId = userId, FailedLoginAttempts = failedLoginAttempts, IsActive = isActive });
        }
        public void AddUserProducts(int userId, List<int> productIds)
        {
            if (productIds == null || productIds.Count == 0)
                return;

            var sql = new StringBuilder();
            sql.Append("INSERT INTO UserProducts (UserId, ProductId) VALUES ");
            var parameters = new DynamicParameters();
            for (int i = 0; i < productIds.Count; i++)
            {
                sql.Append($"(@UserId, @ProductId{i})");
                if (i < productIds.Count - 1)
                {
                    sql.Append(", ");
                }
                parameters.Add($"ProductId{i}", productIds[i]);
            }
            parameters.Add("UserId", userId);

            _dbConnection.Execute(sql.ToString(), parameters);
        }

        public async Task<IEnumerable<UsersInfoDto>> GetAllUsers()
        {
            var sql = "SELECT * FROM UsersInfo";
            var users = await _dbConnection.QueryAsync<UsersInfoDto>(sql);
            return users;
        }

       
    }
}
