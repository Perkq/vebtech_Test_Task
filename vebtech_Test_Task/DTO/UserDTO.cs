using vebtech_Test_Task.Models;

namespace vebtech_Test_Task.DTO
{
    public class UserDTO
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public ICollection<RoleDTO> Roles { get; set; }

        public UserDTO()
        {

        }

        public UserDTO(string name, int age, string email, ICollection<Role> roles)
        {
            Name = name;
            Age = age;
            Email = email;
            Roles = new List<RoleDTO>();

            roles.ToList().ForEach((role) => Roles.Add(new RoleDTO(role.RoleId)));
        }

    }
}
