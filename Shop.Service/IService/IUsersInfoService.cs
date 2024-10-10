using Shop.DataAccess.DTOs;
using Shop.DataAccess.Models;

namespace Shop.Service.IService
{
    public interface IUsersInfoService
    {
        Task<IEnumerable<UsersInfoDto>> GetAllUsers();
        Task<UsersInfoDto> GetUserById(Guid usersInfoGuid);
      
        void UpdateUser(UsersInfoDto userDto);
        void DeleteUser(int id);
        UsersInfoDto Authenticate(string username, string password);

        public void AddUserProducts(UserProductSelectionDto userProductSelection);

        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<RegisterDto> RegisterAsync(RegisterDto registerDto);
        Task<RegisterDto> GetUserByUsernameAsync(string username);
    }
}
 