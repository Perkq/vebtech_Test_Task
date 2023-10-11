using vebtech_Test_Task.Models;
using vebtech_Test_Task.Enums;

namespace vebtech_Test_Task.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleByIdAsync(RoleEnum roleId);

    }
}
