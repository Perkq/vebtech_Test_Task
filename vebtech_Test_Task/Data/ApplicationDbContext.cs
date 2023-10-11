using Microsoft.EntityFrameworkCore;
using vebtech_Test_Task.Enums;
using vebtech_Test_Task.Models;

namespace vebtech_Test_Task.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
               .HasMany(e => e.Users)
               .WithMany(e => e.Roles)
               .UsingEntity("UsersToRolesJoinTable");

            modelBuilder.Entity<Role>().HasData(
                new Role(RoleEnum.User, "User"),
                new Role(RoleEnum.Admin, "Admin"),
                new Role(RoleEnum.Support, "Support"),
                new Role(RoleEnum.SuperAdmin, "SuperAdmin")
            );
        }
    }
}
