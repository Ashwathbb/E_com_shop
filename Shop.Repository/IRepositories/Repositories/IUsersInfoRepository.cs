using Shop.DataAccess.DTOs;

namespace Shop.Repository.IRepositories.Repositoriescc
{
    public interface IUsersInfoRepository
    {
        void AddFailedLoginAttempt(int userId, DateTime attemptTime);
        Task<CreateUserDto> AddUser(CreateUserDto entity);
        Task AddUserAndDepartmentAsync(UsersInfoDepartment usersInfoDepartment);
        void AddUserProducts(int userId, List<int> productIds);
        Task AddUsersInfoAsync(UsersInfoDepartment usersInfoDto);
        void DeleteUser(int id);
        Task<IEnumerable<UsersInfoDto>> GetAllUsers();
        Task<IEnumerable<UsersInfoDepartment>> GetAllUsersAsync();
        Task<UsersInfoDto> GetUserById(Guid userId);
        UsersInfoDto GetUserByUsername(string username, string password);
        void UpdateUser(UsersInfoDto entity);
        void UpdateUserLoginAttempts(int userId, int failedLoginAttempts);
        void UpdateUserLoginAttemptsAndBlock(int userId, int failedLoginAttempts, bool isActive);
    }
}