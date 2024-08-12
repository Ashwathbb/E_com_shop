using Shop.DataAccess.DTOs;
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
    }
}

