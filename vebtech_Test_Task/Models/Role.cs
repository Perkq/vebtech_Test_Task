using vebtech_Test_Task.Enums;

namespace vebtech_Test_Task.Models
{
    public class Role
    {
        
        public RoleEnum RoleId { get; set; }

        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }

        public Role()
        {

        }

        public Role(RoleEnum Role, string role)
        {
            RoleId = Role;
            RoleName = role;
            Users = new List<User>();
        }

    }
}
