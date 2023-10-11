using vebtech_Test_Task.Data;
using vebtech_Test_Task.Repositories.Interfaces;
using vebtech_Test_Task.Models;
using vebtech_Test_Task.Enums;
using Microsoft.EntityFrameworkCore;

namespace vebtech_Test_Task.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext dbContext;

        public RoleRepository(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public async Task<Role> GetRoleByIdAsync(RoleEnum roleId) 
        {
            return await dbContext.Roles.FirstOrDefaultAsync(x => x.RoleId == roleId);
        }
    }
}
