using vebtech_Test_Task.Models;

namespace vebtech_Test_Task.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetUserByIdAsync(int id);

        public Task<List<User>> GetUsersByNameAsync(string name);

        public Task<List<User>> GetUsersByAgeAsync(int age);

        public Task<User> GetUserByEmailAsync(string Email);

        public Task<List<User>> GetAllUsersAsync();

        public Task<User> AddNewRoleAsync(User user, Role role);

        public Task<User> CreateUserAsync(User newUser);

        public Task<User> UpdateUserAsync(User userToUpdate, User updatedUser);

        public Task<User> DeleteUserAsync(User userToDelete);

        public Task<List<User>> GetSortedByNameAsync();

        public Task<List<User>> GetSortedByAgeAsync();

        public Task<List<User>> GetSortedByEmailAsync();

        
    }
}
