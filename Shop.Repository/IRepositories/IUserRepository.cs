using Shop.DataAccess.DTOs;
using Shop.DataAccess.Models;
using System.Threading.Tasks;

namespace Shop.Repository.IRepositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UsersInfoDto>> GetAllUsersAsync();
        Task<UsersInfoDto> GetUserById(Guid UsersInfoGuid);

        Task AddUser(CreateUserDto entity);
        Task AddUsersInfoAsync(UsersInfoDepartment usersInfoDto);
        Task AddUserAndDepartmentAsync(UsersInfoDepartment usersInfoDepartment);
        void UpdateUser(UsersInfoDto entity);
        void DeleteUser(int id);
        UsersInfoDto GetUserByUsername(string username, string password);
        // void UpdateUserIsActive(int userId, bool isActive);
        void AddFailedLoginAttempt(int userId, DateTime attemptTime);
        void UpdateUserLoginAttempts(int userId, int failedLoginAttempts);
        void UpdateUserLoginAttemptsAndBlock(int userId, int failedLoginAttempts, bool isActive);
        void AddUserProducts(int userId, List<int> productIds);
        Task<IEnumerable<UsersInfoDto>> GetAllUsers();
        //for registraion and login
        Task<RegisterDto> GetUserByUsernameAsync(string username);
        Task<RegisterDto> RegisterUserAsync(RegisterDto user, string password);
        Task<LoginRole> GetRoleByNameAsync(string roleName);
        Task AddUserAsync(UsersInfo user);
    }
}

