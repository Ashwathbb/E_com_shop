using Shop.DataAccess.DTOs;

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
    }
}
 