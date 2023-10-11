using Microsoft.EntityFrameworkCore;
using vebtech_Test_Task.Data;
using vebtech_Test_Task.Models;
using vebtech_Test_Task.Repositories.Interfaces;

namespace vebtech_Test_Task.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepository(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await Task.FromResult(dbContext.Users.Include(x => x.Roles).First(x => x.Id == id));
        }

        public async Task<List<User>> GetUsersByNameAsync(string name)
        {
            return await Task.FromResult(dbContext.Users.Include(x => x.Roles).Where(x => x.Name == name).ToList());
        }

        public async Task<List<User>> GetUsersByAgeAsync(int age)
        {
            return await Task.FromResult(dbContext.Users.Include(x => x.Roles).Where(x => x.Age == age).ToList());
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await Task.FromResult(dbContext.Users.Include(x => x.Roles).First(x => x.Email == email));
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await Task.FromResult(dbContext.Users.Include(x => x.Roles).ToList());
        }

        public async Task<User> AddNewRoleAsync(User user, Role role)
        {
            dbContext.Users.Where(x => x.Id == user.Id).First().Roles.Add(role);
            await dbContext.SaveChangesAsync();
            return await Task.FromResult(dbContext.Users.First(x => x.Id == user.Id));
        }

        public async Task<User> CreateUserAsync(User newUser)
        {
            dbContext.Users.Add(newUser);
            await dbContext.SaveChangesAsync();
            return await Task.FromResult(dbContext.Users.First(x => x.Id == newUser.Id));
        }

        public async Task<User> UpdateUserAsync(User userToUpdate, User updatedUser)
        {
            dbContext.Entry(userToUpdate).CurrentValues.SetValues(updatedUser);
            await dbContext.SaveChangesAsync();
            return await Task.FromResult(dbContext.Users.First(x => x.Id == updatedUser.Id));
        }

        public async Task<User> DeleteUserAsync(User userToDelete)
        {
            dbContext.Users.Remove(userToDelete);
            await dbContext.SaveChangesAsync();
            return await Task.FromResult(userToDelete);
        }

        public async Task<List<User>> GetSortedByNameAsync()
        {
            return await Task.FromResult(dbContext.Users.Include(x => x.Roles).OrderBy(x => x.Name).ToList());
        }

        public async Task<List<User>> GetSortedByAgeAsync()
        {
            return await Task.FromResult(dbContext.Users.Include(x => x.Roles).OrderBy(x => x.Age).ToList());
        }

        public async Task<List<User>> GetSortedByEmailAsync()
        {
            return await Task.FromResult(dbContext.Users.Include(x => x.Roles).OrderBy(x => x.Email).ToList());
        }
    }
}
