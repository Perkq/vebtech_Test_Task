using vebtech_Test_Task.Models;
using vebtech_Test_Task.DTO;

namespace vebtech_Test_Task.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDTO> GetUserByIdAsync(int id);

        public Task<List<UserDTO>> GetUsersByNameAsync(string name);

        public Task<List<UserDTO>> GetUsersByAgeAsync(int age);

        public Task<UserDTO> GetUserByEmailAsync(string Email);

        public Task<List<UserDTO>> GetAllUsersAsync();

        public Task<UserDTO> AddNewRoleAsync(int userId, RoleDTO role);

        public Task<UserDTO> CreateUserAsync(UserDTO newUser);

        public Task<UserDTO> UpdateUserAsync(int id, UserDTO updatedUser);

        public Task<UserDTO> DeleteUserAsync(int id);

        public Task<List<UserDTO>> GetSortedByNameAsync();

        public Task<List<UserDTO>> GetSortedByAgeAsync();

        public Task<List<UserDTO>> GetSortedByEmailAsync();

        public List<PageUsers> PaginateUsers(List<UserDTO> users, int pageSize);

    }
}
