namespace vebtech_Test_Task.Models
{
    public class User
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public ICollection<Role> Roles { get; set; }

        public User()
        {

        }

        public User(int id, string name, int age, string email)
        {
            Id = id;
            Name = name;
            Age = age;
            Email = email;
            Roles = new List<Role>();
        }

        public User(int id, string name, int age, string email, ICollection<Role> roles)
        {
            Id = id;
            Name = name;
            Age = age;
            Email = email;
            Roles = roles;
        }

    }
}
